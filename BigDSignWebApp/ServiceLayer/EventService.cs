using BigDSignClientDesktop.Service;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class EventService : IEventService
	{
		private readonly IServiceConnection _eventService;
        // TODO: Put into config
        private readonly string _serviceBaseUrl = "https://localhost:7253/api/events";

		public EventService()
		{
			_eventService = new ServiceConnection(_serviceBaseUrl);
		}

		public async Task<IEnumerable<Event>> GetEvents()
		{
			IEnumerable<Event> events;
			_eventService.Url = $"{_serviceBaseUrl}";
			HttpResponseMessage serviceResponse = await _eventService.CallServiceGet();
			string responseData = await serviceResponse.Content.ReadAsStringAsync();
			APIResult<IEnumerable<Event>>? apiResult = JsonConvert.DeserializeObject<APIResult<IEnumerable<Event>>>(responseData);
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
	}
}
