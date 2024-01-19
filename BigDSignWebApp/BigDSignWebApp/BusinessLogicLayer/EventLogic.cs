using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigDSignWebApp.ModelConversion;
using BigDSignWebApp.ViewModels;
using ModelLayer;
using ServiceLayer;

namespace BusinessLogicLayer
{
    public class EventLogic : IEventLogic
    {
        private readonly IEventService _eventService;

        public EventLogic(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IEnumerable<EventModel>> GetEvents()
        {
            return (await _eventService.GetEvents()).Select(e => e.ToEventModel());
        }
    }
}
