using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IEventControl
    {
        Task<Event> GetEvent(int id);
        Task<IEnumerable<Event>> GetEvents();
        Task<bool> CreateEvent(Event e);
        Task<bool> DeleteEvent(int id);
        Task<bool> UpdateEvent(Event e, int id);
    }
}
