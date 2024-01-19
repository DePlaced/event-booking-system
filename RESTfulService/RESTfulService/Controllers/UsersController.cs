using Microsoft.AspNetCore.Mvc;
using RESTfulService.BusinesslogicLayer;
using RESTfulService.DTOs;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using Data.Exceptions;

namespace RESTfulService.Controllers
{

	/// <summary>
	/// API controller for managing Users.
	/// </summary>
	[ApiController]
	[Route("api/users")]
	[Produces("application/json")]
	public class UsersController : ControllerBase
	{
		public class AuthenticateBody
		{
			public AuthenticateBody(string username, string password)
			{
				Username = username;
				Password = password;
			}

			public string Username { get; set; }
			public string Password { get; set; }
		}

		private readonly IUserLogic _userLogic;

		public UsersController(IUserLogic userLogic)
		{
			_userLogic = userLogic;
		}

		/// <summary>
		/// Authenticates a user.
		/// </summary>
		/// <param name="body">The authentication request containing username and password.</param>
		/// <response code="200">Returns the authenticated user's details</response>
		/// <response code="400">Bad request if the username or password is incorrect</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/users/authenticate
		[HttpPost("Authenticate")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<APIResult> Authenticate([FromBody] AuthenticateBody body)
		{
			try
			{
				UserDTO? userDTO = _userLogic.Authenticate(body.Username, body.Password);
				if (userDTO is not null)
				{
					return Ok(APIResult.WithData(userDTO));
				}
				else
				{
					return BadRequest(APIResult.WithError("Incorrect username or password."));
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
		/// Registers a new user.
		/// </summary>
		/// <param name="userDTO">JSON object representing the user to be registered.</param>
		/// <response code="201">User created successfully</response>
		/// <response code="400">Bad request if invalid data is provided</response>
		/// <response code="500">Internal Server Error: An unexpected error occurred on the server.</response>
		// URI: api/users
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<APIResult> Register([FromBody] UserDTO userDTO)
		{
			try
			{
				_userLogic.Register(userDTO);
				return StatusCode(StatusCodes.Status201Created, APIResult.Empty());
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
