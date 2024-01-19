using BigDSignClientDesktop.Service;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class BookingLineService : IBookingLineService
	{
		private readonly IServiceConnection _bookingLineService;
        // TODO: Put into config
        private readonly string _serviceBaseUrl = "https://localhost:7253/api/booking-lines";

		public BookingLineService()
		{
			_bookingLineService = new ServiceConnection(_serviceBaseUrl);
		}

		public async Task<int> CreateBookingLine(BookingLine bookingLine)
		{
			int bookingLineId;
			_bookingLineService.Url = $"{_serviceBaseUrl}";
			string postJson = JsonConvert.SerializeObject(bookingLine);
			StringContent postContent = new StringContent(postJson, Encoding.UTF8, "application/json");
			HttpResponseMessage serviceResponse = await _bookingLineService.CallServicePost(postContent);
			string responseData = await serviceResponse.Content.ReadAsStringAsync();
			APIResult<int?>? apiResult = JsonConvert.DeserializeObject<APIResult<int?>>(responseData);
			if (serviceResponse.IsSuccessStatusCode && apiResult?.Data is not null)
			{
				bookingLineId = (int)apiResult.Data;
			}
			else
			{
				throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
			}
			return bookingLineId;
		}

		public async Task<bool> DeleteBookingLine(int id)
		{
			bool deleted;
			_bookingLineService.Url = $"{_serviceBaseUrl}/{id}";
			HttpResponseMessage serviceResponse = await _bookingLineService.CallServiceDelete();
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

		public async Task<BookingLine?> GetBookingLine(int id)
		{
			BookingLine? bookingLine;
			_bookingLineService.Url = $"{_serviceBaseUrl}/{id}";
			HttpResponseMessage serviceResponse = await _bookingLineService.CallServiceGet();
			string responseData = await serviceResponse.Content.ReadAsStringAsync();
			APIResult<BookingLine>? apiResult = JsonConvert.DeserializeObject<APIResult<BookingLine>>(responseData);
			if ((serviceResponse.IsSuccessStatusCode || serviceResponse.StatusCode == System.Net.HttpStatusCode.NotFound))
			{
				bookingLine = apiResult?.Data;
			}
			else
			{
				throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
			}
			return bookingLine;
		}

		public async Task<IEnumerable<BookingLine>> GetBookingLinesByBookingId(int bookingId)
		{
			IEnumerable<BookingLine> bookingLines;
			_bookingLineService.Url = $"{_serviceBaseUrl}?bookingId={bookingId}";
			HttpResponseMessage serviceResponse = await _bookingLineService.CallServiceGet();
			string responseData = await serviceResponse.Content.ReadAsStringAsync();
			APIResult<IEnumerable<BookingLine>>? apiResult = JsonConvert.DeserializeObject<APIResult<IEnumerable<BookingLine>>>(responseData);
			if (serviceResponse.IsSuccessStatusCode && apiResult?.Data is not null)
			{
				bookingLines = apiResult.Data;
			}
			else
			{
				throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
			}
			return bookingLines;
		}

		public async Task<bool> UpdateBookingLine(int id, BookingLine bookingLine)
		{
			bool updated;
			_bookingLineService.Url = $"{_serviceBaseUrl}/{id}";
			string putJson = JsonConvert.SerializeObject(bookingLine);
			StringContent putContent = new StringContent(putJson, Encoding.UTF8, "application/json");
			HttpResponseMessage serviceResponse = await _bookingLineService.CallServicePut(putContent);
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
