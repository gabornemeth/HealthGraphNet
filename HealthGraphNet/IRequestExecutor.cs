using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthGraphNet
{
    public interface IRequestExecutor
    {
        Task<T> Execute<T>(IRestRequest request, string baseUrl = null, HttpStatusCode? expectedStatusCode = null) where T : new();
        /// <summary>
        /// Synchronous request.  baseUrl is optional and may be assigned if restClient eneds to be anything other than ApiBaseUrl.
        /// Throws a HealthGraphException if response is anything other than OK.
        /// No data is returned.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="baseUrl"></param>
        Task Execute(IRestRequest request, string baseUrl = null, HttpStatusCode? expectedStatusCode = null);

        /// <summary>
        /// Synchronous request for creation of a resource.  Returns the Location header if present, otherwise returns null.
        /// Throws a HealthGraphException if response is anything other than CREATED.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headerNameOfInterest"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        Task<string> ExecuteCreate(IRestRequest request, string baseUrl = null);
    }
}
