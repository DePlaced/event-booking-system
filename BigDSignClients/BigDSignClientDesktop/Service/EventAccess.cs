using BigDSignClientDesktop.Model;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignClientDesktop.Service
{
    /// <summary>
    /// Provides access to Event-related data through HTTP requests.
    /// </summary>
    public class EventAccess : IEventAccess
    {
        private readonly IServiceConnection _eventService;
        // TODO: Put into config
        private readonly string _serviceBaseUrl = "https://localhost:7253/api/events";

        /// <summary>
        /// Initializes a new instance of the <see cref="EventAccess"/> class.
        /// </summary>
        public EventAccess()
        {
            _eventService = new ServiceConnection(_serviceBaseUrl);
        }

        /// <summary>
        /// Deletes an event with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>True if the event is deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteEvent(int id)
        {
            bool deleted;
            _eventService.Url = $"{_serviceBaseUrl}/{id}";
            HttpResponseMessage serviceResponse = await _eventService.CallServiceDelete();
            string responseData = await serviceResponse.Content.ReadAsStringAsync();
            APIResult? apiResult = JsonConvert.DeserializeObject<APIResult>(responseData);
            if (serviceResponse.IsSuccessStatusCode)
            {
                deleted = true;
            }
            else if (serviceResponse.StatusCode == HttpStatusCode.NotFound)
            {
                deleted = false;
            }
            else
            {
                throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
            }
            return deleted;
        }

        /// <summary>
        /// Retrieves events from the service based on the provided sign ID.
        /// </summary>
        /// <param name="id">The ID of the event to retrieve.</param>
        /// <returns>The list of retrieved events.</returns>
        public async Task<List<Event>> GetEventsBySignId(int signId)
        {
            List<Event> events;
            _eventService.Url = $"{_serviceBaseUrl}?signId={signId}";
            HttpResponseMessage serviceResponse = await _eventService.CallServiceGet();
            string responseData = await serviceResponse.Content.ReadAsStringAsync();
            APIResult<List<Event>>? apiResult = JsonConvert.DeserializeObject<APIResult<List<Event>>>(responseData);
            if (serviceResponse.IsSuccessStatusCode && apiResult?.Data is not null)
            {
                events = apiResult.Data;
            }
            else
            {
                throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
            }
            return events;
        }

        /// <summary>
        /// Saves a new event to the service.
        /// </summary>
        /// <param name="eventToSave">The event object to save.</param>
        /// <returns>The ID of the inserted event.</returns>
        public async Task<int> SaveEvent(Event eventToSave)
        {
            int eventId;
            _eventService.Url = $"{_serviceBaseUrl}";
            string postJson = JsonConvert.SerializeObject(eventToSave);
            StringContent postContent = new StringContent(postJson, Encoding.UTF8, "application/json");
            HttpResponseMessage serviceResponse = await _eventService.CallServicePost(postContent);
            string responseData = await serviceResponse.Content.ReadAsStringAsync();
            APIResult<int?>? apiResult = JsonConvert.DeserializeObject<APIResult<int?>>(responseData);
            if (serviceResponse.IsSuccessStatusCode && apiResult?.Data is not null)
            {
                eventId = (int)apiResult.Data;
            }
            else
            {
                throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
            }
            return eventId;
        }

        /// <summary>
        /// Updates an existing event.
        /// </summary>
        /// <param name="eventToUpdate">The event object containing updated information.</param>
        /// <returns>True if the event is updated successfully; otherwise, false.</returns>
        public async Task<bool> UpdateEvent(Event eventToUpdate)
        {
            bool updated;
            _eventService.Url = $"{_serviceBaseUrl}/{eventToUpdate.Id}";
            string putJson = JsonConvert.SerializeObject(eventToUpdate);
            StringContent putContent = new StringContent(putJson, Encoding.UTF8, "application/json");
            HttpResponseMessage serviceResponse = await _eventService.CallServicePut(putContent);
            string responseData = await serviceResponse.Content.ReadAsStringAsync();
            APIResult? apiResult = JsonConvert.DeserializeObject<APIResult>(responseData);
            if (serviceResponse.IsSuccessStatusCode)
            {
                updated = true;
            }
            else if (serviceResponse.StatusCode == HttpStatusCode.NotFound)
            {
                updated = false;
            }
            else
            {
                throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
            }
            return updated;
        }
    }
}
