using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SignData;
using SignData.DatabaseLayer;
using SignData.ModelLayer;
using System.ComponentModel;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SignData.Tests;

public class ConcurrencyIssueSolvedTest
{
	private readonly ISignAccess _signAccess;
	private readonly IStadiumAccess _stadiumAccess;
	private readonly IEventAccess _eventAccess;
	private readonly IBookingAccess _bookingAccess;
	private readonly IBookingLineAccess _bookingLineAccess;
	private readonly ITestOutputHelper _outputHelper;

	public ConcurrencyIssueSolvedTest(ITestOutputHelper output)
	{
		_outputHelper = output;
		IConfiguration inConfig = TestConfigHelper.GetIConfigurationRoot();
		_eventAccess = new EventAccess(inConfig);
		_bookingAccess = new BookingAccess(inConfig);
		_bookingLineAccess = new BookingLineAccess(inConfig);
	}

	private async Task<int> ExecuteConcurrencyIssueCreateBookingAsync(int userId, int time = 0)
	{
		//Fetches all events
		List<Event> readList = await _eventAccess.GetAllEvents();
		List<Event> useList = new();
		int bookingId = 0;
		
		//Filter away all events that are sold out
		foreach (Event readEvent in readList)
		{
			if (!readEvent.AvailabilityStatus.Equals("Sold out"))
			{
				useList.Add(readEvent);
			}
		}
		//Creates a list of booking lines for the booking
		List<BookingLine> bookingLines = new()
		{
			new BookingLine(useList.First().Price, 0, useList.First().Id, useList.First()),
		};
		//Creates the booking and adds booking lines
		Booking bookingToAdd = new(0, DateTime.Now, "Confirmed", 500, userId, bookingLines);
		try
		{
			//Creates booking with a added timer, that sleeps the thread for one of the users in the middle of the transaction
			bookingId = await _bookingAccess.CreateBooking(bookingToAdd, time);
		}
		catch
		{
		}
		return bookingId;
	}

	private async Task<Event> ExecuteConcurrencyIssueUpdateEventAsync(decimal eventPrice, int time = -1)
	{
		List<Event> readList = await _eventAccess.GetAllEvents();
		List<Event> useList = new();
		foreach (Event readEvent in readList)
		{
			if (!readEvent.AvailabilityStatus.Equals("Sold out"))
			{
				useList.Add(readEvent);
			}
		}
		if (time > 0)
		{
			await Task.Delay(time); // Use Task.Delay instead of Thread.Sleep
		}
		Event eventToUpdate = useList.First();
		eventToUpdate.Price = eventPrice;
		await _eventAccess.UpdateEvent(eventToUpdate.Id, eventToUpdate);
		return await _eventAccess.GetEventById(eventToUpdate.Id);
	}


	/// <summary>
	/// Used to test if the concurrency issue with multiple users booking the same event has been fixed, user 1 and user 2 view the event list, user 2 book the event immidietly while user 1 waits 4 seconds before booking
	/// </summary>
	[Fact]
	public async Task MultipleUsersBookingSameEvent()
	{
		// Arrange
		int user1 = 1;
		int user2 = 2;
		int time = 10000;

		// Act
		Task<int> user1Task = Task.Run(() => ExecuteConcurrencyIssueCreateBookingAsync(user1, time));
		Task<int> user2Task = Task.Run(() => ExecuteConcurrencyIssueCreateBookingAsync(user2));

		// Await completion of both tasks
		await Task.WhenAll(user1Task, user2Task);

		// Retrieve results from tasks
		int bookingIdUser1 = user1Task.Result;
		int bookingIdUser2 = user2Task.Result;

		// Assert
		// Retrieve booking lines and bookings
		List<BookingLine> bookingLinesUser1 = await _bookingLineAccess.GetBookingLinesByBookingId(bookingIdUser1);
		List<BookingLine> bookingLinesUser2 = await _bookingLineAccess.GetBookingLinesByBookingId(bookingIdUser2);
		Booking? bookingUser1 = await _bookingAccess.GetBookingById(bookingIdUser1);
		Booking? bookingUser2 = await _bookingAccess.GetBookingById(bookingIdUser2);

		// Perform assertions
		Assert.Null(bookingUser1);
		Assert.NotNull(bookingUser2);
		Assert.Empty(bookingLinesUser1);
		Assert.NotEmpty(bookingLinesUser2);

		// Output details
		_outputHelper.WriteLine($"First users booking found: {bookingUser1 != null}");
		_outputHelper.WriteLine($"Second users booking found: {bookingUser2 != null}");
		_outputHelper.WriteLine($"{bookingLinesUser1.Count} booking lines are made for first user");
		_outputHelper.WriteLine($"{bookingLinesUser2.Count} booking lines are made for second user");

		// Clean up: Delete bookings after assertions
		await _bookingAccess.DeleteBookingById(bookingIdUser2);
	}

	/// <summary>
	/// Used to test if the concurrency issue with user booking and event, that has been updated between viewing and booking the event, user view the event list, admin updates the same event, user waits 4 seconds before booking
	/// </summary>
	[Fact]
	public async Task UserBookingAdminUpdatingEvent()
	{
		// Arrange
		int userId = 1;
		int time = 4000;
		decimal eventPrice = 9999;

		// Act
		Task<int> userTask = Task.Run(() => ExecuteConcurrencyIssueCreateBookingAsync(userId, time));
		Task<Event> adminTask = Task.Run(() => ExecuteConcurrencyIssueUpdateEventAsync(eventPrice));

		// Await completion of both tasks
		await Task.WhenAll(userTask, adminTask);

		// Retrieve results from tasks
		int bookingIdUser = userTask.Result;
		Event updatedEvent = adminTask.Result;

		// Assert
		// Retrieve booking lines and booking for user
		List<BookingLine> userBookingLines = await _bookingLineAccess.GetBookingLinesByBookingId(bookingIdUser);
		Booking? userBooking = await _bookingAccess.GetBookingById(bookingIdUser);


		// Perform assertions
		Assert.Equal(updatedEvent.Price, eventPrice);
		Assert.Empty(userBookingLines);
		Assert.Null(userBooking);

		// Output details
		_outputHelper.WriteLine($"Admin has updated event price to: {updatedEvent.Price}");
		_outputHelper.WriteLine($"The booking succeded: {bookingIdUser > 0}");
		_outputHelper.WriteLine($"User did not book event: {userBooking == null}, while admin updated event: {updatedEvent.Id}");

		// Clean up: Delete bookings after assertions
		await _bookingAccess.DeleteBookingById(bookingIdUser);
		updatedEvent.Price = 100;
		await _eventAccess.UpdateEvent(updatedEvent.Id, updatedEvent);
	}
}