
using BigDSignClientDesktop.Model;
using BigDSignClientDesktop.Service;

namespace BigDSignClientDesktop.Controller
{
    /// <summary>
    /// Controller handling event-related operations, acting as an   between the UI and data access.
    /// </summary>
    public class EventControl
    {
        readonly IEventAccess _eAccess;

        /// <summary>
        /// Constructor initializing the EventControl with an EventAccess instance.
        /// </summary>
        public EventControl()
        {
            _eAccess = new EventAccess();
        }

        /// <summary>
        /// Retrieves events associated with a specific sign and date from the data source.
        /// </summary>
        /// <param name="eventDate">The ID of the sign to filter events by.</param>
        /// <param name="signId">The date of the event to filter events by.</param>
        /// <returns>A task that represents a list of events related to the given sign and date, or null if none are found.</returns>
        public async Task<List<Event>> GetAllEventsWithDate(DateTime eventDate, int signId)
        {
            // TODO: Implement API filter to use instead.
            // Get all events and filter them based on event date.
            List<Event> foundEvents = (await GetEventsBySignId(signId)).Where(e => e.EventDate.Date == eventDate.Date).ToList();
            return foundEvents;
        }

        /// <summary>
        /// Retrieves events associated with a specific sign from the data source.
        /// </summary>
        /// <param name="signId">The ID of the sign to filter events by.</param>
        /// <returns>A task that represents a list of events related to the given sign, or null if none are found.</returns>
        public async Task<List<Event>> GetEventsBySignId(int signId)
        {
            List<Event> foundEvents = await _eAccess.GetEventsBySignId(signId);
            return foundEvents;
        }

        /// <summary>
        /// Saves a new event with the provided details to the data source.
        /// </summary>
        /// <returns>A task that represents the ID of the inserted event.</returns>
        public async Task<int> SaveEvent(string name, DateTime eventDate, string description, decimal price, string availabilityStatus, int signId)
        {
            Event newEvent = new Event(name, eventDate, description, price, availabilityStatus, signId);
            int insertedId = await _eAccess.SaveEvent(newEvent);
            return insertedId;
        }

        /// <summary>
        /// Deletes an event with the specified ID from the data source.
        /// </summary>
        /// <param name="eventIdToDelete">The ID of the event to delete.</param>
        /// <returns>A task that represents a boolean indicating the success of the deletion operation.</returns>
        public async Task<bool> DeleteEvent(int id)
        {
            bool eventDeleted = await _eAccess.DeleteEvent(id);
            return eventDeleted;
        }

        /// <summary>
        /// Updates an existing event in the data source with the provided event details.
        /// </summary>
        /// <param name="eventToUpdate">The event object containing updated information.</param>
        /// <returns>A task that represents a boolean indicating the success of the update operation.</returns>
        public async Task<bool> UpdateEvent(Event eventToUpdate)
        {
            bool updated = await _eAccess.UpdateEvent(eventToUpdate);
            return updated;
        }
    }
}
