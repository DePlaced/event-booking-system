using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignClientDesktop.Service
{
    /// <summary>
    /// An API result that can an error message.
    /// </summary>
    public class APIResult
    {
        /// <summary>
        /// The error message.
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// T
        /// </summary>
        /// <param name="statusCode"></param>
        /// <exception cref="ServiceException"></exception>
        public void ThrowErrorAsServiceException(HttpStatusCode statusCode)
        {
            if (Error is null)
            {
                throw new ServiceException("Internal server error.", HttpStatusCode.InternalServerError);
            }
            else
            {
                throw new ServiceException(Error, statusCode);
            }
        }
    }

    /// <summary>
    /// An API result that can data or an error message.
    /// </summary>
    public class APIResult<T> : APIResult
    {
        /// <summary>
        /// The data.
        /// </summary>
        public T? Data { get; set; }
    }
}
