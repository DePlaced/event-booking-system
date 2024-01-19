using System;
using Microsoft.AspNetCore.Mvc;
using RESTfulService.BusinesslogicLayer;
using RESTfulService.DTOs;
using Data.Exceptions;

namespace RESTfulService.Controllers
{
	/// <summary>
	/// API controller for managing Bookings.
	/// </summary>
	[ApiController]
	[Route("api/bookings")]
	[Produces("application/json")]
	public class BookingsController : ControllerBase
    {
        private readonly IBookingLogic _bookingLogic;

        public BookingsController(IBookingLogic bookingLogic)
        {
            _bookingLogic = bookingLogic;
        }

		/// <summary>
		/// Retrieves a specific booking by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the booking.</param>
		/// <response code="200">Returns the found booking</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="404">If the booking wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/bookings/{id}
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Get([FromRoute]int id)
        {
            try
            {
				BookingDTO? foundBooking = await _bookingLogic.Get(id);
                return foundBooking != null ? Ok(APIResult.WithData(foundBooking)) : NotFound(APIResult.WithError("Booking not found."));
            }
			// Catch exceptions caused by a bad request.
			catch (BadRequestException exception)
			{
				return BadRequest(APIResult.WithError(exception.Message));
			}
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
        }

		/// <summary>
		/// Retrieves all bookings by their userId, no input returns 405 and no bookings reltaed to userId found returns empty collection.
		/// </summary>
		/// <param name="userId">The userId used to filter bookings, no input returns empty collection.</param>
		/// <response code="200">Returns a list of bookings</response>
		/// <response code="400">Bad request if invalid userId is provided in filter</response>
		/// <response code="405">At the moment method not allowed without input</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/bookings?userId=1
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> GetAll([FromQuery] int? userId)
        {
            try
            {
				if (userId.HasValue)
				{
					List<BookingDTO> foundBookings = await _bookingLogic.GetAllByUserId(userId.Value);
					return Ok(APIResult.WithData(foundBookings));
				}
				else
				{
					return StatusCode(405);
				}

            }
			// Catch exceptions caused by a bad request.
			catch (BadRequestException exception)
			{
				return BadRequest(APIResult.WithError(exception.Message));
			}
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				return StatusCode(StatusCodes.Status500InternalServerError, APIResult.WithInternalServerError());
			}
		}


		/// <summary>
		/// Creates a new booking.
		/// </summary>
		/// <param name="bookingDTO">JSON object representing the booking.</param>
		/// <response code="201">Booking created successfully</response>
		/// <response code="400">Bad request</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/bookings
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Create([FromBody] BookingDTO bookingDTO)
        {
            try
            {
                int insertedId = await _bookingLogic.Post(bookingDTO);
                return Created($"api/bookings/{insertedId}", APIResult.WithData(insertedId));
            }
			// Catch exceptions caused by a bad request.
			catch (BadRequestException exception)
			{
				return BadRequest(APIResult.WithError(exception.Message));
			}
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				return StatusCode(StatusCodes.Status500InternalServerError, APIResult.WithInternalServerError());
			}
		}

		/// <summary>
		/// Updates an existing booking by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the booking.</param>
		/// <param name="bookingDTO">JSON object representing the updated booking.</param>
		/// <response code="200">Booking updated successfully</response>
		/// <response code="400">Bad request if data was invalid</response>
		/// <response code="404">If the booking wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/bookings/{id}
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Update([FromRoute] int id, [FromBody] BookingDTO bookingDTO)
        {
            try
            {
                bool updated = await _bookingLogic.Put(id, bookingDTO);
                return updated ? Ok(APIResult.Empty()) : NotFound(APIResult.WithError("Booking for update not found."));
            }
			// Catch exceptions caused by a bad request.
			catch (BadRequestException exception)
			{
				return BadRequest(APIResult.WithError(exception.Message));
			}
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				return StatusCode(StatusCodes.Status500InternalServerError, APIResult.WithInternalServerError());
			}
		}

		/// <summary>
		/// Deletes a booking by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the booking.</param>
		/// <response code="200">Booking deleted successfully</response>
		/// <response code="400">Bad request if id is invalid</response>
		/// <response code="404">If the booking wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URL: api/bookings/{id}
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Delete([FromRoute]int id)
        {
            try
            {
                bool deleted = await _bookingLogic.Delete(id);
                return deleted ? Ok(APIResult.Empty()) : NotFound(APIResult.WithError("Booking for deletion not found."));
            }
			// Catch exceptions caused by a bad request.
			catch (BadRequestException exception)
			{
				return BadRequest(APIResult.WithError(exception.Message));
			}
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				return StatusCode(StatusCodes.Status500InternalServerError, APIResult.WithInternalServerError());
			}
		}
    }
}

