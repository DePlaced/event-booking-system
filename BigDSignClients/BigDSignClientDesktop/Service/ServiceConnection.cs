using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignClientDesktop.Service
{
    public class ServiceConnection : IServiceConnection
    {
        private readonly HttpClient _httpClient;

        public ServiceConnection(string url)
        {
            Url = url;
            _httpClient = new HttpClient();
        }

        public string Url { get; set; }

        public async Task<HttpResponseMessage> CallServiceGet()
        {
            return await _httpClient.GetAsync(Url);
        }

        public async Task<HttpResponseMessage> CallServicePost(StringContent postContent)
        {
            return await _httpClient.PostAsync(Url, postContent);
        }

        public async Task<HttpResponseMessage> CallServicePut(StringContent putContent)
        {
            return await _httpClient.PutAsync(Url, putContent);
        }

        public async Task<HttpResponseMessage> CallServiceDelete()
        {
            return await _httpClient.DeleteAsync(Url);
        }
    }
}
