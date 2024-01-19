using BigDSignWebApp.ViewModels;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IEventLogic
    {
        Task<IEnumerable<EventModel>> GetEvents();
    }
}
