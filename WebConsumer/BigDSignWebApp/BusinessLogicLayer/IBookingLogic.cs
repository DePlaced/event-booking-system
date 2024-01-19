using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigDSignWebApp.ViewModels;
using ModelLayer;
using Newtonsoft.Json.Serialization;

namespace BusinessLogicLayer
{
    public interface IBookingLogic
    {
        void AddEventToBooking(ISession session, int eventId, string eventName, decimal price, byte[] rowId, Int64 rowIdBig);

        void ClearBookingEvents(ISession session);

        Task<int> CreateBooking(ISession session, int signRenterId);

        Task<bool> DeleteBooking(int id);

        List<EventModel> FetchEventForViewList(ISession session);

        Task<BookingModel?> GetBooking(int id);

        Task<IEnumerable<BookingModel>> GetBookingsByUserId(int userId);

        Task<bool> UpdateBooking(int id, BookingModel bookingModel);
    }
}
