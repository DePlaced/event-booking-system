using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	[Serializable]
	public class ServiceException : Exception
	{
		public static ServiceException InternalServerError()
		{
			return new ServiceException("Internal server error.", HttpStatusCode.InternalServerError);
		}

		public ServiceException()
		{

		}

		public ServiceException(string message) : base(message)
		{

		}

		public ServiceException(string message, Exception innerException) : base(message, innerException)
		{

		}

		public ServiceException(string message, HttpStatusCode statusCode) : base(message)
		{
			StatusCode = statusCode;
		}

		public HttpStatusCode StatusCode { get; set; }
	}
}
