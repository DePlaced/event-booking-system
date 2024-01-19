using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer;
using BigDSignWebApp.ModelConversion;
using BigDSignWebApp.Cookies;
using BigDSignWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ModelLayer;
using System.Diagnostics;
using ServiceLayer;

namespace BigDSignWebApp.Controllers
{
	[Authorize]
	public class BookingsController : BaseController
	{
		private readonly IBookingLogic _bookingLogic;
		private readonly IBookingLineLogic _bookingLineLogic;
		private readonly IEventLogic _eventLogic;

		public BookingsController(ICookieProvider cookieProvider, IBookingLogic bookingLogic, IBookingLineLogic bookingLineLogic, IEventLogic eventLogic) : base(cookieProvider)
		{
			_bookingLogic = bookingLogic;
			_bookingLineLogic = bookingLineLogic;
			_eventLogic = eventLogic;
		}

		public async Task<ActionResult> BookingList()
		{
			int userId = CookieProvider.GetCookieUser()!.Id;
			try
			{
				IEnumerable<BookingModel> bookingModels = await _bookingLogic.GetBookingsByUserId(userId);
				return View(bookingModels);
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

		// GET: Bookings/Details/{id}
		public async Task<ActionResult> BookingDetails([FromRoute] int id)
		{
			try
			{
				IEnumerable<BookingLineModel> bookingLinesModels = await _bookingLineLogic.GetBookingLinesByBookingId(id);
				return View(bookingLinesModels);
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

		// GET: Bookings/Delete/{id}
		public ActionResult Delete([FromRoute] int id)
		{
			_bookingLogic?.DeleteBooking(id);
			return RedirectToAction(nameof(BookingList));
		}

		// POST: Bookings/Delete/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete([FromRoute] int id, IFormCollection collection)
		{
			return RedirectToAction(nameof(Index));
		}
	}
}
