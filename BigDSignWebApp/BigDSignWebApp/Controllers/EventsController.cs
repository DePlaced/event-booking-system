using BigDSignWebApp.ModelConversion;
using BusinessLogicLayer;
using BigDSignWebApp.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using BigDSignWebApp.ViewModels;
using ServiceLayer;

namespace BigDSignWebApp.Controllers
{
	[Authorize]
	public class EventsController : BaseController
	{
		private readonly IEventLogic _eventLogic;
		private readonly IBookingLogic _bookingLogic;

		public EventsController(ICookieProvider cookieProvider, IEventLogic eventLogic, IBookingLogic bookingLogic) : base(cookieProvider)
		{
			_eventLogic = eventLogic;
			_bookingLogic = bookingLogic;
		}

		// GET: /Events
		public async Task<ActionResult> EventList()
		{
			try
			{
				IEnumerable<EventModel> eventList = await _eventLogic.GetEvents();
				List<EventModel> selectedEvents = _bookingLogic.FetchEventForViewList(HttpContext.Session);
				ViewBag.SelectedEvents = selectedEvents;
				return View(eventList);
			}
			// Catch exceptions returned from the API service.
			catch (ServiceException exception)
			{
				ShowAlert("danger", $"{(int)exception.StatusCode} {exception.Message}");
				return View();
			}
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				ShowAlert("danger", $"500 Internal server error.");
				return View();
			}
		}

		// GET: /Events/Details/{id}
		public ActionResult Details([FromRoute] int id)
		{
			return View();
		}

		// GET: /Events/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: /Events/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			return RedirectToAction(nameof(Index));
		}

		// GET: /Events/Edit/{id}
		public ActionResult Edit([FromRoute] int id)
		{
			return View();
		}

		// POST: /Events/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([FromRoute] int id, IFormCollection collection)
		{
			return RedirectToAction(nameof(Index));
		}

		// GET: /Events/Delete/{id}
		public ActionResult Delete([FromRoute] int id)
		{
			return View();
		}

		// POST: /Events/Delete/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete([FromRoute] int id, IFormCollection collection)
		{
			return RedirectToAction(nameof(Index));
		}

		// POST: /Events/AddEventToBooking
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEventToBooking([FromForm] int eventId, [FromForm] string eventName, [FromForm] decimal price, [FromForm] byte[] rowId, [FromForm] Int64 rowIdBig)
		{
			_bookingLogic.AddEventToBooking(HttpContext.Session, eventId, eventName, price, rowId, rowIdBig);
			return RedirectToAction(nameof(EventList));
		}

		// POST: /Events/ClearBookingEvents
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClearBookingEvents()
		{
			_bookingLogic.ClearBookingEvents(HttpContext.Session);
			return RedirectToAction(nameof(EventList));
		}

		// POST: /Events/CreateBooking
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBooking()
		{
			int userId = CookieProvider.GetCookieUser()!.Id;
			try
			{
				await _bookingLogic.CreateBooking(HttpContext.Session, userId);
				ShowAlert("success", "Booking succeded.");
			}
			// Catch exceptions returned from the API service.
			catch (ServiceException exception)
			{
				ShowAlert("danger", $"{(int)exception.StatusCode} {exception.Message}");
			}
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				ShowAlert("danger", $"500 Internal server error.");
			}
			return RedirectToAction(nameof(EventList));
		}
	}
}
