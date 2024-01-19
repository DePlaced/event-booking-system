using BigDSignRestfulService.DTOs;

namespace BigDSignRestfulService.BusinesslogicLayer
{
	public interface IStadiumLogic
	{
		/// <summary>
		///Get Stadium by ID
		/// </summary>
		Task<StadiumDTO?> Get(int id);

		/// <summary>
		///Get All Stadiums
		/// </summary>
		Task<List<StadiumDTO>> GetAll();

		/// <summary>
		///Create Stadium
		/// </summary>
		Task<int> Add(StadiumDTO stadiumToAdd);

		/// <summary>
		///Update Stadium by ID
		/// </summary>
		Task<bool> Put(int id, StadiumDTO stadiumToUpdate);

		/// <summary>
		///Delete Stadium by ID
		/// </summary>
		Task<bool> Delete(int id);
	}
}
