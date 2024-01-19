using RESTfulService.DTOs;
using Data.DatabaseLayer;
using Data.ModelLayer;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using RESTfulService.ModelConversion;

namespace RESTfulService.BusinesslogicLayer
{
	/// <summary>
	/// Service class for handling operations related to event entities.
	/// </summary>
	public class EventLogic : IEventLogic
	{
		/// <summary>
		/// The data access layer for event entities.
		/// </summary>
		private readonly IEventAccess _eventAccess;

		/// <summary>
		/// Initializes a new instance of the EventService class.
		/// </summary>
		/// <param name="eventAccess">The event access object used for database operations.</param>
		public EventLogic(IEventAccess eventAccess)
		{
			_eventAccess = eventAccess;
		}

		/// <summary>
		///Create method for EventService.
		/// </summary>
		public async Task<int> Add(EventDTO eventToAdd)
		{
			return await _eventAccess.CreateEvent(eventToAdd.ToEvent());
		}

		/// <summary>
		///Delete Event by ID
		/// </summary>
		public async Task<bool> Delete(int id)
		{
			// Delete the event using the data access layer.
			return await _eventAccess.DeleteEventById(id);
		}

		/// <summary>
		///Get Event by ID
		/// </summary>
		public async Task<EventDTO?> Get(int id)
		{
			// Retrieve the event using the data access layer.
			Event? foundEvent = await _eventAccess.GetEventById(id);

			// If the event is found, convert it to a DTO and return.
			if (foundEvent != null)
			{
				return foundEvent.ToEventDTO();
			}
			return null;
		}

		/// <summary>
		///Get All Events
		/// </summary>
		public async Task<List<EventDTO>> GetAll(int signId = -1)
		{
			List<Event> foundEvents;
			if (signId == -1)
			{
				// Retrieve all events using the data access layer.
				foundEvents = await _eventAccess.GetAllEvents();
			}
			else
			{
				// Retrieve all events using the data access layer.
				foundEvents = await _eventAccess.GetAllEventsBySignId(signId);
			}
			// Convert to DTO list
			List<EventDTO> foundEventDTOs = foundEvents.Select(e => e.ToEventDTO()).ToList();
			return foundEventDTOs;
		}

		/// <summary>
		///Update Event by ID
		/// </summary>
		public async Task<bool> Put(int id, EventDTO eventToUpdate)
		{
			return await _eventAccess.UpdateEvent(id, eventToUpdate.ToEvent());
		}
	}
}
