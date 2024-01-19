using System.Collections.Generic;
using System.Threading.Tasks;
using BigDSignClientDesktop.Model;

namespace BigDSignClientDesktop.Service
{
    /// <summary>
    /// Interface for accessing event-related data from a service.
    /// </summary>
    public interface IEventAccess
    {
        /// <summary>
        /// Retrieves a list of events from the service based on the provided sign ID.
        /// </summary>
        /// <param name="id">ID of the sign.</param>
        /// <returns>List of events.</returns>
        Task<List<Event>> GetEventsBySignId(int signId);

        /// <summary>
        /// Saves an event to the service.
        /// </summary>
        /// <param name="eventToSave">The event object to be saved.</param>
        /// <returns>The ID of the inserted event.</returns>
        Task<int> SaveEvent(Event eventToSave);

        /// <summary>
        /// Deletes an event from the service.
        /// </summary>
        /// <param name="eventId">The ID of the event to delete.</param>
        /// <returns>True if the event was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteEvent(int id);

        /// <summary>
        /// Updates an existing event in the service.
        /// </summary>
        /// <param name="eventToUpdate">The event object with updated information.</param>
        /// <returns>True if the event was updated successfully; otherwise, false.</returns>
        Task<bool> UpdateEvent(Event eventToUpdate);
    }
}
