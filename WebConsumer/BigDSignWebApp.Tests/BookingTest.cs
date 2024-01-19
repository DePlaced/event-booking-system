using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ServiceLayer;
using Xunit.Abstractions;
using BigDSignWebApp.Utilities;
using ModelLayer;
using BigDSignWebApp.ViewModels;
using Microsoft.AspNetCore.Razor.TagHelpers;
using BigDSignWebApp.ModelConversion;

namespace BigDSignWebApp.Tests
{
	public class BookingTest
	{
		private readonly IBookingLogic _bookingLogic;
		private readonly ITestOutputHelper _outputHelper;
		private readonly IBookingService _bookingService;
		private readonly IBookingLineService _bookingLineService;
		private readonly IBookingLineLogic _bookingLineLogic;
		private readonly IEventLogic _eventLogic;
		private readonly IEventService _eventService;

		public BookingTest(ITestOutputHelper output)
		{
			_outputHelper = output;
			_bookingService = new BookingService();
			_bookingLogic = new BookingLogic(_bookingService);
			_bookingLineService = new BookingLineService();
			_bookingLineLogic = new BookingLineLogic(_bookingLineService);
			_eventService = new EventService();
			_eventLogic = new EventLogic(_eventService);
		}

		[Fact]
		public async void AddEventToList()
		{
			//Arrange
			ISession _session = new SessionStub();
			IEnumerable<EventModel> eventListModel = await _eventLogic.GetEvents();
			Event @event = eventListModel.First().ToEvent();

			//Act
			_bookingLogic.AddEventToBooking(_session, @event.Id, @event.EventName, @event.Price, @event.RowId, @event.RowIdBig);

			//Assert
			List<Event> eventList = _session.GetObjectFromJson<List<Event>>("eventList");
			Event lastEvent = eventList.Last();
			Assert.Equal(@event.Id, lastEvent.Id);
			Assert.Equal(@event.Price, lastEvent.Price);

			//Test output
			_outputHelper.WriteLine($"Event with id: {@event.Id} was added to session");
		}

		[Fact]
		public async void CreateBooking()
		{
			//Arrange
			ISession _session = new SessionStub();
			IEnumerable<EventModel> eventListModel = await _eventLogic.GetEvents();
			Event event1 = eventListModel.First().ToEvent();
			Event event2 = eventListModel.Last().ToEvent();
			int signRenterId = 1;

			//Act
			_bookingLogic.AddEventToBooking(_session, event1.Id, event1.EventName, event1.Price, event1.RowId, event1.RowIdBig);
			_bookingLogic.AddEventToBooking(_session, event2.Id, event2.EventName, event2.Price, event2.RowId, event2.RowIdBig);
			int bookingId = await _bookingLogic.CreateBooking(_session, signRenterId);

			//Assert
			BookingModel? foundBooking = await _bookingLogic.GetBooking(bookingId);
			Assert.NotNull(foundBooking);
			foundBooking.Lines = (await _bookingLineLogic.GetBookingLinesByBookingId(bookingId)).ToList();
			BookingLineModel firstBookingLine = foundBooking.Lines.First();
			BookingLineModel lastBookingLine = foundBooking.Lines.Last();

			Assert.Equal(signRenterId, foundBooking.SignRenterId);
			Assert.Equal(event1.Id, firstBookingLine.EventId);
			Assert.Equal(event2.Id, lastBookingLine.EventId);
			Assert.Equal(event1.Price, firstBookingLine.LineEvent.Price);
			Assert.Equal(event2.Price, lastBookingLine.LineEvent.Price);
			Assert.Equal("Sold out", firstBookingLine.LineEvent.AvailabilityStatus);
			Assert.Equal("Sold out", lastBookingLine.LineEvent.AvailabilityStatus);
			Assert.Equal(event1.Price + event2.Price, foundBooking.TotalPrice);

			//Cleanup
			await _bookingLineLogic.DeleteBookingLine(bookingId);

			//Test output
			_outputHelper.WriteLine($"Booking was added with id: {foundBooking.Id}");
			_outputHelper.WriteLine($"It has {foundBooking.Lines.Count} booking lines");
			_outputHelper.WriteLine($"The events added are {event1.EventName} and {event2.EventName}");
		}
	}
}
