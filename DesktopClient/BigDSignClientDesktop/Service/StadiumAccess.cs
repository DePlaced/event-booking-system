using BigDSignClientDesktop.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignClientDesktop.Service
{
    /// <summary>
    /// Provides access to Stadium-related data through HTTP requests.
    /// </summary>
    public class StadiumAccess : IStadiumAccess
    {
        private readonly IServiceConnection _stadiumService;
        // TODO: Put into config
        private readonly string _serviceBaseUrl = "https://localhost:7253/api/stadiums";

        /// <summary>
        /// Initializes a new instance of the <see cref="StadiumAccess"/> class.
        /// </summary>
        public StadiumAccess()
        {
            _stadiumService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<Stadium?> GetStadium(int id)
        {
            Stadium? stadium;
            _stadiumService.Url = $"{_serviceBaseUrl}/{id}";
            HttpResponseMessage serviceResponse = await _stadiumService.CallServiceGet();
            string responseData = await serviceResponse.Content.ReadAsStringAsync();
            APIResult<Stadium>? apiResult = JsonConvert.DeserializeObject<APIResult<Stadium>>(responseData);
            if ((serviceResponse.IsSuccessStatusCode || serviceResponse.StatusCode == System.Net.HttpStatusCode.NotFound))
            {
                stadium = apiResult?.Data;
            }
            else
            {
                throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
            }
            return stadium;
        }

        /// <summary>
        /// Retrieves all stadiums from the service.
        /// </summary>
        /// <returns>The list of retrieved stadiums.</returns>
        public async Task<List<Stadium>> GetAllStadiums()
        {
            List<Stadium> stadiums;
            _stadiumService.Url = $"{_serviceBaseUrl}";
            HttpResponseMessage serviceResponse = await _stadiumService.CallServiceGet();
            string responseData = await serviceResponse.Content.ReadAsStringAsync();
            APIResult<List<Stadium>>? apiResult = JsonConvert.DeserializeObject<APIResult<List<Stadium>>>(responseData);
            if (serviceResponse.IsSuccessStatusCode && apiResult?.Data is not null)
            {
                stadiums = apiResult.Data;
            }
            else
            {
                throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
            }
            return stadiums;
        }

        /// <summary>
        /// Saves a new stadium to the service.
        /// </summary>
        /// <param name="stadiumToSave">The stadium object to save.</param>
        /// <returns>The ID of the inserted stadium.</returns>
        public async Task<int> SaveStadium(Stadium stadiumToSave)
        {
            int stadiumId;
            _stadiumService.Url = $"{_serviceBaseUrl}";
            string postJson = JsonConvert.SerializeObject(stadiumToSave);
            StringContent postContent = new StringContent(postJson, Encoding.UTF8, "application/json");
            HttpResponseMessage serviceResponse = await _stadiumService.CallServicePost(postContent);
            string responseData = await serviceResponse.Content.ReadAsStringAsync();
            APIResult<int?>? apiResult = JsonConvert.DeserializeObject<APIResult<int?>>(responseData);
            if (serviceResponse.IsSuccessStatusCode && apiResult?.Data is not null)
            {
                stadiumId = (int)apiResult.Data;
            }
            else
            {
                throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
            }
            return stadiumId;
        }

        /// <summary>
        /// Deletes a stadium with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the stadium to delete.</param>
        /// <returns>True if the stadium is deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteStadium(int id)
        {
            bool deleted;
            _stadiumService.Url = $"{_serviceBaseUrl}/{id}";
            HttpResponseMessage serviceResponse = await _stadiumService.CallServiceDelete();
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

        /// <summary>
        /// Updates an existing stadium.
        /// </summary>
        /// <param name="stadiumToUpdate">The stadium object containing updated information.</param>
        /// <returns>True if the stadium is updated successfully; otherwise, false.</returns>
        public async Task<bool> UpdateStadium(Stadium stadiumToUpdate)
        {
            bool updated;
            _stadiumService.Url = $"{_serviceBaseUrl}/{stadiumToUpdate.Id}";
            string putJson = JsonConvert.SerializeObject(stadiumToUpdate);
            StringContent putContent = new StringContent(putJson, Encoding.UTF8, "application/json");
            HttpResponseMessage serviceResponse = await _stadiumService.CallServicePut(putContent);
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
