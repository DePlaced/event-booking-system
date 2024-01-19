using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace ServiceLayer
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEvents();
    }
}
