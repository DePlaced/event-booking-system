using ModelLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigDSignWebApp.ViewModels;
using BigDSignWebApp.ModelConversion;

namespace BusinessLogicLayer
{
    public class BookingLineLogic : IBookingLineLogic
    {
        private readonly IBookingLineService _bookingLineService;

        public BookingLineLogic(IBookingLineService bookingLineService)
        {
            _bookingLineService = bookingLineService;
        }

        public async Task<int> CreateBookingLine(BookingLineModel bookingLineModel)
        {
            return await _bookingLineService.CreateBookingLine(bookingLineModel.ToBookingLine());
        }

        public async Task<bool> DeleteBookingLine(int id)
        {
           return await _bookingLineService.DeleteBookingLine(id);
        }

        public async Task<BookingLineModel?> GetBookingLine(int id)
        {
            return (await _bookingLineService.GetBookingLine(id))?.ToBookingLineModel();
        }

        public async Task<IEnumerable<BookingLineModel>> GetBookingLinesByBookingId(int bookingId)
        {
            return (await _bookingLineService.GetBookingLinesByBookingId(bookingId)).Select(line => line.ToBookingLineModel());
        }

        public async Task<bool> UpdateBookingLine(int id, BookingLineModel bookingLineModel)
        {
            return await _bookingLineService.UpdateBookingLine(id, bookingLineModel.ToBookingLine());
        }
    }
}
