using BigDSignRestfulService.DTOs;
using SignData.DatabaseLayer;
using SignData.ModelLayer;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using BigDSignRestfulService.ModelConversion;

namespace BigDSignRestfulService.BusinesslogicLayer
{
	/// <summary>
	/// Service class for handling operations related to stadium entities.
	/// </summary>
	public class StadiumLogic : IStadiumLogic
	{
		/// <summary>
		/// The data access layer for stadium entities.
		/// </summary>
		private readonly IStadiumAccess _stadiumAccess;

		/// <summary>
		/// Initializes a new instance of the StadiumService class.
		/// </summary>
		/// <param name="stadiumAccess">The stadium access object used for database operations.</param>
		public StadiumLogic(IStadiumAccess stadiumAccess)
		{
			_stadiumAccess = stadiumAccess;
		}

		/// <summary>
		///Create Stadium
		/// </summary>
		public async Task<int> Add(StadiumDTO stadiumToAdd)
		{
			return await _stadiumAccess.CreateStadium(stadiumToAdd.ToStadium());
		}

		/// <summary>
		///Delete Stadium by ID
		/// </summary>
		public async Task<bool> Delete(int id)
		{
			// Delete the stadium using the data access layer.
			return await _stadiumAccess.DeleteStadiumById(id);
		}

		/// <summary>
		///Get Stadium by ID
		/// </summary>
		public async Task<StadiumDTO?> Get(int id)
		{
			// Retrieve the stadium using the data access layer.
			Stadium? foundStadium = await _stadiumAccess.GetStadiumById(id);

			// If the stadium is found, convert it to a DTO and return.
			if (foundStadium != null)
			{
				return foundStadium.ToStadiumDTO();
			}
			return null;
		}

		/// <summary>
		///Get All Stadiums
		/// </summary>
		public async Task<List<StadiumDTO>> GetAll()
		{
			// Retrieve all stadiums using the data access layer.
			List<Stadium> foundStadiums = await _stadiumAccess.GetAllStadiums();
			// Convert the DTOs.
			List<StadiumDTO> foundStadiumDTOs = foundStadiums.Select(s => s.ToStadiumDTO()).ToList();
			return foundStadiumDTOs;
		}

		/// <summary>
		///Update Stadium by ID
		/// </summary>
		public async Task<bool> Put(int id, StadiumDTO stadiumToUpdate)
		{
			return await _stadiumAccess.UpdateStadium(id, stadiumToUpdate.ToStadium());
		}
	}
}
