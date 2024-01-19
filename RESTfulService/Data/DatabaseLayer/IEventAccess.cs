using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.ModelLayer;

namespace Data.DatabaseLayer
{
    public interface IEventAccess
    {
        Task<Event?> GetEventById(int id);

        Task<List<Event>> GetAllEvents();

        Task<List<Event>> GetAllEventsBySignId(int signId);

        Task<int> CreateEvent(Event eventToAdd);

        Task<bool> UpdateEvent(int id, Event eventToUpdate);

        Task<bool> DeleteEventById(int id);
    }
}
