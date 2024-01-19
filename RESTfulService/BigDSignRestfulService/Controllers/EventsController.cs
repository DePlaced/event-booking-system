using Microsoft.AspNetCore.Mvc;
using BigDSignRestfulService.BusinesslogicLayer;
using BigDSignRestfulService.DTOs;
using System.Collections.Generic;
using SignData.Exceptions;

namespace BigDSignRestfulService.Controllers
{

	/// <summary>
	/// API controller for managing Events.
	/// </summary>
	[ApiController]
	[Route("api/events")]
	[Produces("application/json")]
	public class EventsController : ControllerBase
    {
        private readonly IEventLogic _eventLogic;

        public EventsController(IEventLogic eventLogic)
        {
            _eventLogic = eventLogic;
        }

		/// <summary>
		/// Retrieves a specific event by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the event.</param>
		/// <response code="200">Returns the found event</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="404">If the event wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/events/{id}
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Get([FromRoute]int id)
        {
            try
            {
				EventDTO? foundEvent = await _eventLogic.Get(id);
                return foundEvent != null ? Ok(APIResult.WithData(foundEvent)) : NotFound(APIResult.WithError("Event not found."));
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
		/// Retrieves all events by their signId or all events if no signId is provided, empty collection if not events are found by signId.
		/// </summary>
		/// <param name="signId">The signId used to filter events, if provided.</param>
		/// <response code="200">Returns a list of events, returns empty collection if no events with signId found</response>
		/// <response code="400">Bad request if invalid signId is provided</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/events or api/events?signId={signId}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> GetAll([FromQuery] int? signId)
        {
            try
            {
                if (signId.HasValue)
                {
					List<EventDTO> foundEvents = await _eventLogic.GetAll(signId.Value);
                    return Ok(APIResult.WithData(foundEvents));
                }
                else
                {
					List<EventDTO> allEvents = await _eventLogic.GetAll();
                    return Ok(APIResult.WithData(allEvents));
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
		/// Creates a new event.
		/// </summary>
		/// <param name="eventDTO">JSON object representing the event.</param>
		/// <response code="201">Event created successfully</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/events
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Create([FromBody] EventDTO eventDTO)
        {
            try
            {
                int insertedId = await _eventLogic.Add(eventDTO);
                return Created($"api/events/{insertedId}", APIResult.WithData(insertedId));
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
		/// Updates an event by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the event.</param>
		/// <param name="eventDTO">JSON object representing the updated event.</param>
		/// <response code="200">Event updated successfully</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="404">If the event wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/events/{id}
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Update([FromRoute] int id, [FromBody] EventDTO eventDTO)
        {
            try
            {
                bool updated = await _eventLogic.Put(id, eventDTO);
                return updated ? Ok(APIResult.Empty()) : NotFound(APIResult.WithError("Event for update not found."));
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
		/// Deletes an event by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the event.</param>
		/// <response code="200">Event deleted successfully</response>
		/// <response code="400">Bad request if invalid id is provided</response>
		/// <response code="404">If the event wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/events/{id}
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Delete([FromRoute] int id)
        {
            try
            {
                bool deleted = await _eventLogic.Delete(id);
                return deleted ? Ok(APIResult.Empty()) : NotFound(APIResult.WithError("Event for deletion not found."));
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
