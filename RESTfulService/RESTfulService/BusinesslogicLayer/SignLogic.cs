using RESTfulService.DTOs;
using RESTfulService.ModelConversion;
using Microsoft.Extensions.Configuration;
using Data.DatabaseLayer;
using Data.ModelLayer;
using System;
using System.Collections.Generic;

namespace RESTfulService.BusinesslogicLayer
{
	/// <summary>
	/// Service class for handling operations related to sign entities.
	/// </summary>
	public class SignLogic : ISignLogic
	{
		/// <summary>
		/// The data access layer for sign entities.
		/// </summary>
		private readonly ISignAccess _signAccess;

		/// <summary>
		/// Initializes a new instance of the SignService class.
		/// </summary>
		/// <param name="signAccess">The sign access object used for database operations.</param>
		public SignLogic(ISignAccess signAccess)
		{
			_signAccess = signAccess;
		}

		/// <summary>
		/// Create Sign
		/// </summary>
		public async Task<int> Add(SignDTO newSign)
		{
			return await _signAccess.CreateSign(newSign.ToSign());
		}

		/// <summary>
		///Delete Sign
		/// </summary>
		public async Task<bool> Delete(int id)
		{
			// Delete the sign using the data access layer.
			return await _signAccess.DeleteSignById(id);
		}

		/// <summary>
		///Get Sign by ID
		/// </summary>
		public async Task<SignDTO?> Get(int id)
		{
			// Retrieve the sign using the data access layer.
			Sign? foundSign = await _signAccess.GetSignById(id);
			return foundSign?.ToSignDTO();
		}

		/// <summary>
		/// Get All Signs
		/// </summary>
		public async Task<List<SignDTO>> GetAll(int stadiumId)
		{
			// Retrieve all signs using the data access layer.
			List<Sign> foundSigns = await _signAccess.GetAllSigns(stadiumId);
			// Convert to DTOs.
			List<SignDTO> foundSignDTOs = foundSigns.Select(s => s.ToSignDTO()).ToList();
			return foundSignDTOs;
		}

		/// <summary>
		///Update Sign by ID
		/// </summary>
		public async Task<bool> Put(int id, SignDTO signToUpdate)
		{
			return await _signAccess.UpdateSign(id, signToUpdate.ToSign());
		}
	}
}
