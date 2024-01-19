using System;
using Microsoft.AspNetCore.Mvc;
using BigDSignRestfulService.BusinesslogicLayer;
using BigDSignRestfulService.DTOs;
using SignData.Exceptions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BigDSignRestfulService.Controllers
{
	/// <summary>
	/// API controller for managing BookingLines.
	/// </summary>
	[ApiController]
	[Route("api/booking-lines")]
	[Produces("application/json")]
	public class BookingLinesController : ControllerBase
    {
        private readonly IBookingLineLogic _bookingLineLogic;

        /// <summary>
        /// The constructor for the bookingLinesController
        /// </summary>
        /// <param name="bookingLineLogic"></param>
        public BookingLinesController(IBookingLineLogic bookingLineLogic)
        {
            _bookingLineLogic = bookingLineLogic;
        }

		/// <summary>
		/// Retrieves a specific BookingLine by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the BookingLine.</param>
		/// <response code="200">Returns the found booking line</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="404">If the item wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/booking-lines/{id}
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Get([FromRoute]int id)
        {
            try
            {
				BookingLineDTO? foundBookingLine = await _bookingLineLogic.Get(id);
                return foundBookingLine != null ? Ok(APIResult.WithData(foundBookingLine)) : NotFound(APIResult.WithError("Booking line not found."));
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
		/// Retrieves all booking lines by their bookingId, no input return code 405 and no booking lines related to bookingId found returns empty collection.
		/// </summary>
		/// <param name="bookingId">The bookingId used to filter bookingLines, no input returns empty collection.</param>
		/// <response code="200">Returns the list of booking lines.</response>
		/// <response code="400">Bad request if invalid bookingId is provided.</response>
		/// <response code="405">At the moment method not allowed without input</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/booking-lines?bookingId=1
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ResponseCache(Duration = 14400, Location = ResponseCacheLocation.Any)]
		public async Task<ActionResult<APIResult>> GetAll([FromQuery]int? bookingId)
        {
			Response.Headers["Cache-Control"] = "private, max-age=14400, must-revalidate";
			try
            {
				if (bookingId.HasValue)
				{
					List<BookingLineDTO> foundBookingLines = await _bookingLineLogic.GetAll(bookingId.Value);
					return Ok(APIResult.WithData(foundBookingLines));
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
		/// Creates a new BookingLine.
		/// </summary>
		/// <param name="bookingLineDTO">JSON object representing the BookingLine.</param>
		/// <response code="201">Returns the created booking line ID.</response>
		/// <response code="400">Bad request if data is invalid.</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/booking-lines
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Create([FromBody] BookingLineDTO bookingLineDTO)
        {
			try
            {
                int insertedId = await _bookingLineLogic.Add(bookingLineDTO);
                return Created($"api/booking-lines/{insertedId}", APIResult.WithData(insertedId));
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
