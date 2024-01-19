using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignClientDesktop.Service
{
    public interface IServiceConnection
    {
        public string Url { get; set; }

        Task<HttpResponseMessage> CallServiceGet();

        Task<HttpResponseMessage> CallServicePost(StringContent postContent);

        Task<HttpResponseMessage> CallServicePut(StringContent putContent);

        Task<HttpResponseMessage> CallServiceDelete();
    }
}
