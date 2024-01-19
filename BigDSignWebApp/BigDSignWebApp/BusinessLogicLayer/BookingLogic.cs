using BigDSignWebApp.Cookies;
using BigDSignWebApp.ModelConversion;
using BigDSignWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using ModelLayer;
using Newtonsoft.Json;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigDSignWebApp.Utilities;
using Microsoft.AspNetCore.SignalR;

namespace BusinessLogicLayer
{
	public class BookingLogic : IBookingLogic
	{
        private readonly IHubContext<EventHub> _eventHubContext;
        private readonly IBookingService _bookingService;

		public BookingLogic(IBookingService bookingService, IHubContext<EventHub> eventHubContext)
		{
			_bookingService = bookingService;
			_eventHubContext = eventHubContext;
		}

		public BookingLogic(IBookingService bookingService)
		{
			_bookingService = bookingService;
		}

		public List<EventModel> FetchEventForViewList(ISession session)
		{
			var eventList = session.GetObjectFromJson<List<Event>>("eventList");
			if (eventList != null)
			{
				return eventList.Select(@event => @event.ToEventModel()).ToList();
			}
			else
			{
				// Return an empty list if the session data is null
				return new List<EventModel>();
			}
		}

		public void AddEventToBooking(ISession session, int eventId, string eventName, decimal price, byte[] rowId, Int64 rowIdBig)
		{
			var @event = new Event(eventId, eventName, price, rowId, rowIdBig);
			// Get the stored event list JSON from the session and convert it to an event list object.
			List<Event>? eventList = session.GetObjectFromJson<List<Event>>("eventList");
			// If the event list doesn't exist in the session, create it.
			if (eventList == null)
			{
				eventList = new List<Event>();
			}
			// If the event is not already added to the booking, add it.
			if (!eventList.Any(e => e.Id == eventId))
			{
				eventList.Add(@event);
			}
			// Store the modified event list in the session as JSON.
			session.SetObjectAsJson("eventList", eventList);
		}

		public void ClearBookingEvents(ISession session)
		{
			session.Remove("eventList");
		}

		public async Task<int> CreateBooking(ISession session, int signRenterId)
		{
			var events = session.GetObjectFromJson<List<Event>>("eventList");
			if (events == null)
			{
				events = new List<Event>();
			}
			List<BookingLine> tempList = new();
			// Temp price logic
			decimal totalPrice = 0;
			// Fetch the event details
			foreach (Event e in events)
			{
				tempList.Add(new BookingLine(e.Price, e.Id, new Event(e.Price, e.RowId, e.RowIdBig)));
				totalPrice += e.Price; //temp price logic
			}

			// Create a new booking and booking lines
			var newBooking = new Booking
			{
				BookingDate = DateTime.Now,
				// Set an appropriate default status
				BookingStatus = "Confirmed",
				// Temp price logic
				TotalPrice = totalPrice,
				// Assigning user ID
				SignRenterId = signRenterId,
				Lines = tempList
			};

			// Create the booking
			int bookingId = await _bookingService.CreateBooking(newBooking);
            // Loop through each event and update its availability
            foreach (Event e in events)
            {
                await _eventHubContext.Clients.All.SendAsync("UpdateEventAvailability", e.Id, "Sold out");
            }
			// Clear the selected events from the session.
            session.Remove("eventList");

			return bookingId;
		}

		public async Task<bool> DeleteBooking(int id)
		{
			return await _bookingService.DeleteBooking(id);
		}

		public async Task<BookingModel?> GetBooking(int id)
		{
			return (await _bookingService.GetBooking(id))?.ToBookingModel();
		}

		public async Task<IEnumerable<BookingModel>> GetBookingsByUserId(int userId)
		{
			return (await _bookingService.GetBookingsByUserId(userId)).Select(b => b.ToBookingModel());
		}

		public async Task<bool> UpdateBooking(int id, BookingModel bookingModel)
		{
			return await _bookingService.UpdateBooking(id, bookingModel.ToBooking());
		}
	}
}
