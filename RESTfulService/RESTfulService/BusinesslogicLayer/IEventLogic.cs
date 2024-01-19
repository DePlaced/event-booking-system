using RESTfulService.DTOs;

namespace RESTfulService.BusinesslogicLayer
{
	public interface IEventLogic
	{
		/// <summary>
		///Get Event by ID
		/// </summary>
		Task <EventDTO?> Get(int id);

		/// <summary>
		///Get All Events
		/// </summary>
		Task <List<EventDTO>> GetAll(int signsId = -1);

		/// <summary>
		///Create method for EventService.
		/// </summary>
		Task<int> Add(EventDTO eventToAdd);

		/// <summary>
		///Update Event by ID
		/// </summary>
		Task<bool> Put(int id, EventDTO eventToUpdate);

		/// <summary>
		///Delete Event by ID
		/// </summary>
		Task<bool> Delete(int id);
	}
}
