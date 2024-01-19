using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using BigDSignClientDesktop.Model;
using BigDSignClientDesktop.Service;
using System.Text.Json.Serialization;

namespace BigDSignClientDesktop.Service
{
    /// <summary>
    /// Provides access to Sign-related data through HTTP requests.
    /// </summary>
    public class SignAccess : ISignAccess
    {
        private readonly IServiceConnection _signService;
        // TODO: Put into config
        private readonly string _serviceBaseUrl = "https://localhost:7253/api/signs";

        /// <summary>
        /// Initializes a new instance of the <see cref="SignAccess"/> class.
        /// </summary>
        public SignAccess()
        {
            _signService = new ServiceConnection(_serviceBaseUrl);
        }

        /// <summary>
        /// Retrieves signs from the service based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the sign to retrieve. Defaults to -1 to retrieve all signs.</param>
        /// <returns>The list of retrieved signs or null if no signs are found.</returns>
        public async Task<List<Sign>> GetSignsByStadiumId(int stadiumId)
        {
            List<Sign> signs;
            _signService.Url = $"{_serviceBaseUrl}?stadiumId={stadiumId}";
            HttpResponseMessage serviceResponse = await _signService.CallServiceGet();
            string responseData = await serviceResponse.Content.ReadAsStringAsync();
            APIResult<List<Sign>>? apiResult = JsonConvert.DeserializeObject<APIResult<List<Sign>>>(responseData);
            if (serviceResponse.IsSuccessStatusCode && apiResult?.Data is not null)
            {
                signs = apiResult.Data;
            }
            else
            {
                throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
            }
            return signs;
        }

        /// <summary>
        /// Saves a new sign to the service.
        /// </summary>
        /// <param name="signToSave">The sign object to save.</param>
        /// <returns>The ID of the inserted sign or -2 if the insertion fails.</returns>
        public async Task<int> SaveSign(Sign signToSave)
        {
            int eventId;
            _signService.Url = $"{_serviceBaseUrl}";
            string postJson = JsonConvert.SerializeObject(signToSave);
            StringContent postContent = new StringContent(postJson, Encoding.UTF8, "application/json");
            HttpResponseMessage serviceResponse = await _signService.CallServicePost(postContent);
            string responseData = await serviceResponse.Content.ReadAsStringAsync();
            APIResult<int?>? apiResult = JsonConvert.DeserializeObject<APIResult<int?>>(responseData);
            if (serviceResponse.IsSuccessStatusCode && apiResult?.Data is not null)
            {
                eventId = (int)apiResult.Data;
            }
            else
            {
                throw apiResult?.Error is not null ? new ServiceException(apiResult.Error, serviceResponse.StatusCode) : ServiceException.InternalServerError();
            }
            return eventId;
        }

        /// <summary>
        /// Deletes a sign with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the sign to delete.</param>
        /// <returns>True if the sign is deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteSign(int id)
        {
            bool deleted;
            _signService.Url = $"{_serviceBaseUrl}/{id}";
            HttpResponseMessage serviceResponse = await _signService.CallServiceDelete();
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
        /// Updates an existing sign.
        /// </summary>
        /// <param name="signToUpdate">The sign object containing updated information.</param>
        /// <returns>True if the sign is updated successfully; otherwise, false.</returns>
        public async Task<bool> UpdateSign(Sign signToUpdate)
        {
            bool updated;
            _signService.Url = $"{_serviceBaseUrl}/{signToUpdate.Id}";
            string putJson = JsonConvert.SerializeObject(signToUpdate);
            StringContent putContent = new StringContent(putJson, Encoding.UTF8, "application/json");
            HttpResponseMessage serviceResponse = await _signService.CallServicePut(putContent);
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
