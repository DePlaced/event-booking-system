using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ServiceLayer;

namespace BusinessLogicLayer
{
    public class EventControl : IEventControl
    {

        private readonly IEventService _eventService;

        public EventControl(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<bool> CreateEvent(Event e)
        {
            return await _eventService.CreateEvent(e);
        }

        public async Task<bool> DeleteEvent(int id)
        {
            return await _eventService.DeleteEvent(id);
        }

        public async Task<Event> GetEvent(int id)
        {
            return await _eventService.GetEvent(id);
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _eventService.GetEvents();
        }

        public async Task<bool> UpdateEvent(Event e, int id)
        {
            return await _eventService.UpdateEvent(e, id);
        }
    }
}
