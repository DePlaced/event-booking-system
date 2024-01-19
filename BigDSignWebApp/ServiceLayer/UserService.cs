using BigDSignClientDesktop.Service;
using ModelLayer;
using Newtonsoft.Json;
using ServiceLayer;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BusinessLogicLayer
{
	public class UserService : IUserService
	{
		private readonly IServiceConnection _serviceConnection;
		// TODO: Put into config
		private readonly string _serviceBaseUrl = "https://localhost:7253/api/users";

		public UserService()
		{
			_serviceConnection = new ServiceConnection(_serviceBaseUrl);
		}

		public async Task<User?> AuthenticateUser(string username, string password)
		{
			User? user;
			_serviceConnection.Url = $"{_serviceBaseUrl}/authenticate";
			string postJson = JsonConvert.SerializeObject(new
			{
				Username = username,
				Password = password
			});
			StringContent postContent = new StringContent(postJson, Encoding.UTF8, "application/json");
			// Call service with username and password JSON
			HttpResponseMessage serviceResponse = await _serviceConnection.CallServicePost(postContent);
			string responseData = await serviceResponse.Content.ReadAsStringAsync();
			APIResult<User>? apiResult = JsonConvert.DeserializeObject<APIResult<User>>(responseData);
			// if success (code 200-299)
			if (serviceResponse.IsSuccessStatusCode)
			{
				user = apiResult?.Data;
			}
			else
			{
				throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
			}
			return user;
		}

		public async Task RegisterUser(User user)
		{
			_serviceConnection.Url = $"{_serviceBaseUrl}";
			string postJson = JsonConvert.SerializeObject(user);
			StringContent postContent = new StringContent(postJson, Encoding.UTF8, "application/json");
			// Call service with user JSON
			HttpResponseMessage serviceResponse = await _serviceConnection.CallServicePost(postContent);
			string responseData = await serviceResponse.Content.ReadAsStringAsync();
			APIResult? apiResult = JsonConvert.DeserializeObject<APIResult>(responseData);
			// If not created (code 201)
			if (serviceResponse.StatusCode != HttpStatusCode.Created)
			{
				throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
			}
		}
	}
}
