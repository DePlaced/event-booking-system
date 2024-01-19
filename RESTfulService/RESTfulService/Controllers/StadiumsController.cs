using Microsoft.AspNetCore.Mvc;
using RESTfulService.BusinesslogicLayer;
using RESTfulService.DTOs;
using System.Collections.Generic;
using System;
using Data.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RESTfulService.Controllers
{

	/// <summary>
	/// API controller for managing Stadiums.
	/// </summary>
	[ApiController]
	[Route("api/stadiums")]
	[Produces("application/json")]
	public class StadiumsController : ControllerBase
    {
        private readonly IStadiumLogic _stadiumLogic;

        public StadiumsController(IStadiumLogic stadiumLogic)
        {
            _stadiumLogic = stadiumLogic;
        }

		/// <summary>
		/// Retrieves a specific stadium by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the stadium.</param>
		/// <response code="200">Returns the found stadium</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="404">If the stadium wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/stadiums/{id}
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Get([FromRoute] int id)
        {
            try
            {
				StadiumDTO? foundStadium = await _stadiumLogic.Get(id);
                return foundStadium != null ? Ok(APIResult.WithData(foundStadium)) : NotFound(APIResult.WithError("Stadium not found."));
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
		/// Retrieves all stadiums.
		/// </summary>
		/// <response code="200">Returns a list of stadiums</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/stadiums
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> GetAll()
		{
			try
            {
				List<StadiumDTO> foundStadiums = await _stadiumLogic.GetAll();
                return Ok(APIResult.WithData(foundStadiums));
            }
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				return StatusCode(StatusCodes.Status500InternalServerError, APIResult.WithInternalServerError());
			}
		}

		/// <summary>
		/// Creates a new stadium.
		/// </summary>
		/// <param name="stadiumDTO">JSON object representing the stadium.</param>
		/// <response code="201">Stadium created successfully</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/stadiums
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Create([FromBody] StadiumDTO stadiumDTO)
        {
            try
            {
                int insertedId = await _stadiumLogic.Add(stadiumDTO);
                return Created($"api/stadiums/{insertedId}", APIResult.WithData(insertedId));
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
		/// Updates an existing stadium by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the stadium.</param>
		/// <param name="stadiumDTO">JSON object representing the updated stadium.</param>
		/// <response code="200">Stadium updated successfully</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="404">If the stadium wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/stadiums/{id}
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Update([FromRoute] int id, [FromBody] StadiumDTO stadiumDTO)
        {
            try
            {
                bool updated = await _stadiumLogic.Put(id, stadiumDTO);
                return updated ? Ok(APIResult.Empty()) : NotFound(APIResult.WithError("Stadium for update not found."));
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
		/// Deletes a stadium by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the stadium.</param>
		/// <response code="200">Stadium deleted successfully</response>
		/// <response code="400">Bad request if invalid id is provided</response>
		/// <response code="404">If the stadium wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/stadiums/{id}
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Delete([FromRoute] int id)
        {
            try
            {
                bool deleted = await _stadiumLogic.Delete(id);
                return deleted ? Ok(APIResult.Empty()) : NotFound(APIResult.WithError("Stadium for deletion not found."));
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
