using Microsoft.AspNetCore.Mvc;
using BigDSignRestfulService.BusinesslogicLayer;
using BigDSignRestfulService.DTOs;
using BigDSignRestfulService.ModelConversion;
using SignData.ModelLayer;
using System.Collections.Generic;
using SignData.Exceptions;
using System;

namespace BigDSignRestfulService.Controllers
{

	/// <summary>
	/// API controller for managing Signs.
	/// </summary>
	[ApiController]
	[Route("api/signs")]
	[Produces("application/json")]
	public class SignsController : ControllerBase
    {
        private readonly ISignLogic _signLogic;

        public SignsController(ISignLogic signLogic)
        {
            _signLogic = signLogic;
        }

		/// <summary>
		/// Retrieves a specific sign by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the sign.</param>
		/// <response code="200">Returns the found sign</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="404">If the sign wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/signs/{id}
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Get(int id)
        {
            try
            {
                SignDTO? foundSign = await _signLogic.Get(id);
                return foundSign != null ? Ok(APIResult.WithData(foundSign)) : NotFound(APIResult.WithError("Sign not found."));
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
		/// Retrieves all signs related to a specific stadiumId, no input or no signs related to stadiumId found returns empty collection.
		/// </summary>
		/// <param name="stadiumId">The stadiumId used to filter signs, no input returns empty collection.</param>
		/// <response code="200">Returns a list of signs</response>
		/// <response code="400">Bad request if invalid stadiumId is provided</response>
		/// <response code="405">At the moment method not allowed without input</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/signs?stadiumId={stadiumId}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> GetAll([FromQuery]int? stadiumId)
        { 
            try
            {
				if (stadiumId.HasValue)
				{
					List<SignDTO> foundSigns = await _signLogic.GetAll(stadiumId.Value);
					return Ok(APIResult.WithData(foundSigns));
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
		/// Creates a new sign.
		/// </summary>
		/// <param name="signDTO">JSON object representing the sign.</param>
		/// <response code="201">Sign created successfully</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/signs
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Create([FromBody] SignDTO signDTO)
        {
            try
            {
                int insertedId = await _signLogic.Add(signDTO);
                return Created($"api/signs/{insertedId}", APIResult.WithData(insertedId));
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
		/// Updates a sign by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the sign.</param>
		/// <param name="signDTO">JSON object representing the updated sign.</param>
		/// <response code="200">Sign updated successfully</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="404">If the sign wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/signs/{id}
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Update([FromRoute] int id,[FromBody] SignDTO signDTO)
        {
            try
            {
                var updated = await _signLogic.Put(id, signDTO);
                return updated ? Ok(APIResult.Empty()) : NotFound(APIResult.WithError("Sign for update not found."));
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
		/// Deletes a sign by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the sign.</param>
		/// <response code="200">Sign deleted successfully</response>
		/// <response code="400">Bad request if invalid id is provided</response>
		/// <response code="404">If the sign wasn't found</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/signs/{id}
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResult>> Delete([FromRoute]int id)
        {
            try
            {
                var deleted = await _signLogic.Delete(id);
                return deleted ? Ok(APIResult.Empty()) : NotFound(APIResult.WithError("Sign for deletion not found."));
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
