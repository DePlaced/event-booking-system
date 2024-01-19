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
	public class BookingService : IBookingService
	{
		private readonly IServiceConnection _bookingService;
        // TODO: Put into config
        private readonly string _serviceBaseUrl = "https://localhost:7253/api/bookings";

		public BookingService()
		{
			_bookingService = new ServiceConnection(_serviceBaseUrl);
		}

		public async Task<int> CreateBooking(Booking booking)
		{
			int bookingId;
			_bookingService.Url = $"{_serviceBaseUrl}";
			string postJson = JsonConvert.SerializeObject(booking);
			StringContent postContent = new StringContent(postJson, Encoding.UTF8, "application/json");
			HttpResponseMessage serviceResponse = await _bookingService.CallServicePost(postContent);
			string responseData = await serviceResponse.Content.ReadAsStringAsync();
			APIResult<int?>? apiResult = JsonConvert.DeserializeObject<APIResult<int?>>(responseData);
			if (serviceResponse.IsSuccessStatusCode && apiResult?.Data is not null)
			{
				bookingId = (int)apiResult.Data;
			}
			else
			{
				throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
			}
			return bookingId;
		}

		public async Task<bool> DeleteBooking(int id)
		{
			bool deleted;
			_bookingService.Url = $"{_serviceBaseUrl}/{id}";
			HttpResponseMessage serviceResponse = await _bookingService.CallServiceDelete();
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

		public async Task<Booking?> GetBooking(int id)
		{
			Booking? booking;
			_bookingService.Url = $"{_serviceBaseUrl}/{id}";
			HttpResponseMessage serviceResponse = await _bookingService.CallServiceGet();
			string responseData = await serviceResponse.Content.ReadAsStringAsync();
			APIResult<Booking>? apiResult = JsonConvert.DeserializeObject<APIResult<Booking>>(responseData);
			if ((serviceResponse.IsSuccessStatusCode || serviceResponse.StatusCode == System.Net.HttpStatusCode.NotFound))
			{
				booking = apiResult?.Data;
			}
			else
			{
				throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
			}
			return booking;
		}

		public async Task<IEnumerable<Booking>> GetBookingsByUserId(int userId)
		{
			IEnumerable<Booking> bookings;
			_bookingService.Url = $"{_serviceBaseUrl}?userId={userId}";
			HttpResponseMessage serviceResponse = await _bookingService.CallServiceGet();
			string responseData = await serviceResponse.Content.ReadAsStringAsync();
			APIResult<IEnumerable<Booking>>? apiResult = JsonConvert.DeserializeObject<APIResult<IEnumerable<Booking>>>(responseData);
			if (serviceResponse.IsSuccessStatusCode && apiResult?.Data is not null)
			{
				bookings = apiResult.Data;
			}
			else
			{
				throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
			}
			return bookings;
		}

		public async Task<bool> UpdateBooking(int id, Booking booking)
		{
			bool updated;
			_bookingService.Url = $"{_serviceBaseUrl}/{id}";
			string putJson = JsonConvert.SerializeObject(booking);
			StringContent putContent = new StringContent(putJson, Encoding.UTF8, "application/json");
			HttpResponseMessage serviceResponse = await _bookingService.CallServicePut(putContent);
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
