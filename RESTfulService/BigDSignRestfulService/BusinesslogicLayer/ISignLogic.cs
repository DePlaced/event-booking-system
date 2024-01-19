using BigDSignRestfulService.DTOs;

namespace BigDSignRestfulService.BusinesslogicLayer
{
	public interface ISignLogic
	{
		/// <summary>
		///Get Sign by ID
		/// </summary>
		Task<SignDTO?> Get(int id);

		/// <summary>
		/// Get All Signs
		/// </summary>
		Task<List<SignDTO>> GetAll(int stadiumId);

		/// <summary>
		/// Create Sign
		/// </summary>
		Task<int> Add(SignDTO signToAdd);

		/// <summary>
		///Update Sign by ID
		/// </summary>
		Task<bool> Put(int id, SignDTO signToUpdate);

		/// <summary>
		///Delete Sign
		/// </summary>
		Task<bool> Delete(int id);
	}
}