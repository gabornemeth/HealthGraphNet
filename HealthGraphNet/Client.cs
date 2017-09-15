using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using RestSharp.Portable;
using HealthGraphNet.RestSharp;
using System.Threading.Tasks;
using RestSharp.Portable.HttpClient;
using HealthGraphNet.Models;

namespace HealthGraphNet
{
    /// <summary>
    /// HealthGraph API is accessible through this class.
    /// It can still be mocked or stubbed rather easily for testing purposes.
    /// </summary>
    public class Client
    {
        #region Fields and Properties

        internal readonly ISerializer DefaultJsonSerializer = new JsonNETSerializer();
        private const string LocationHeaderName = "Location";

        private const string ApiBaseUrl = "https://api.runkeeper.com";

        protected RestClient RestClient { get; private set; }
        private Func<Task<UsersModel>> _functionGetUser;
        private UsersModel _user; // cache user
        private IRequestExecutor _requestExecutor;

        #endregion

        #region Constructors

        public Client(IAuthenticator authenticator)
        {
            //Initialize the REST client
            RestClient = new RestClient();
            RestClient.ContentHandlers.Clear();
            RestClient.ContentHandlers.Add("*", new JsonNETDeserializer());
            RestClient.Authenticator = authenticator;

            _functionGetUser = () => Task.Run(async () =>
            {
                if (_user == null)
                    _user = await new UsersEndpoint(this).GetUser();
                return _user;
            });
            _requestExecutor = new DefaultRequestExecutor(RestClient);
        }

        protected Client(IAuthenticator authenticator, IRequestExecutor requestExecutor) : this(authenticator)
        {
            if (requestExecutor != null)
                _requestExecutor = requestExecutor;
        }

        #endregion

        private ProfileEndpoint _profileEndPoint;
        /// <summary>
        /// Profile endpoint.
        /// </summary>
        public ProfileEndpoint Profile
        {
            get
            {
                if (_profileEndPoint == null)
                    _profileEndPoint = new ProfileEndpoint(this, _functionGetUser);

                return _profileEndPoint;
            }
        }

        private FitnessActivitiesEndpoint _fitnessActivitiesEndPoint;
        /// <summary>
        /// Profile endpoint.
        /// </summary>
        public FitnessActivitiesEndpoint FitnessActivities
        {
            get
            {
                if (_fitnessActivitiesEndPoint == null)
                    _fitnessActivitiesEndPoint = new FitnessActivitiesEndpoint(this, _functionGetUser);

                return _fitnessActivitiesEndPoint;
            }
        }

        #region IRequest Execution

        private class DefaultRequestExecutor : IRequestExecutor
        {
            private readonly RestClient _restClient;

            public DefaultRequestExecutor(RestClient restClient)
            {
                _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
            }

            /// <summary>
            /// Synchronous request returning data as T.         
            /// baseUrl is optional and may be assigned if restClient needs to be anything other than ApiBaseUrl.
            /// Throws a HealthGraphException if response is anything other than OK.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="request"></param>
            /// <param name="baseUrl"></param>
            /// <returns></returns>
            public async Task<T> Execute<T>(IRestRequest request, string baseUrl = null, HttpStatusCode? expectedStatusCode = null) where T : new()
            {
                if (string.IsNullOrEmpty(baseUrl) == false)
                {
                    _restClient.BaseUrl = new Uri(baseUrl);
                }
                else
                {
                    _restClient.BaseUrl = new Uri(ApiBaseUrl);
                }
                IRestResponse<T> response = await _restClient.Execute<T>(request);
                //If a particular status code is expected, check for it, otherwise assume we are looking for an OK
                HttpStatusCode codeToCheckAgainst = expectedStatusCode.HasValue ? expectedStatusCode.Value : HttpStatusCode.OK;
                if (response.StatusCode != codeToCheckAgainst)
                {
                    throw new HealthGraphException(response);
                }
                return response.Data;
            }

            /// <summary>
            /// Synchronous request.  baseUrl is optional and may be assigned if restClient eneds to be anything other than ApiBaseUrl.
            /// Throws a HealthGraphException if response is anything other than OK.
            /// No data is returned.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="baseUrl"></param>
            public async Task Execute(IRestRequest request, string baseUrl = null, HttpStatusCode? expectedStatusCode = null)
            {
                if (string.IsNullOrEmpty(baseUrl) == false)
                {
                    _restClient.BaseUrl = new Uri(baseUrl);
                }
                else
                {
                    _restClient.BaseUrl = new Uri(ApiBaseUrl);
                }
                IRestResponse response = await _restClient.Execute(request);
                //If a particular status code is expected, check for it, otherwise assume we are looking for an OK
                HttpStatusCode codeToCheckAgainst = expectedStatusCode.HasValue ? expectedStatusCode.Value : HttpStatusCode.OK;
                if (response.StatusCode != codeToCheckAgainst)
                {
                    throw new HealthGraphException(response);
                }
            }

            /// <summary>
            /// Synchronous request for creation of a resource.  Returns the Location header if present, otherwise returns null.
            /// Throws a HealthGraphException if response is anything other than CREATED.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="headerNameOfInterest"></param>
            /// <param name="baseUrl"></param>
            /// <returns></returns>
            public async Task<string> ExecuteCreate(IRestRequest request, string baseUrl = null)
            {
                if (string.IsNullOrEmpty(baseUrl) == false)
                {
                    _restClient.BaseUrl = new Uri(baseUrl);
                }
                else
                {
                    _restClient.BaseUrl = new Uri(ApiBaseUrl);
                }
                IRestResponse response = await _restClient.Execute(request);
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw new HealthGraphException(response);
                }
                IEnumerable<string> locationHeaderValues = null;
                if (response.Headers.TryGetValues(LocationHeaderName, out locationHeaderValues))
                {
                    return locationHeaderValues.First();
                }

                return null;
            }
        }

        /// <summary>
        /// Synchronous request returning data as T.         
        /// baseUrl is optional and may be assigned if restClient needs to be anything other than ApiBaseUrl.
        /// Throws a HealthGraphException if response is anything other than OK.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        internal virtual Task<T> Execute<T>(IRestRequest request, string baseUrl = null, HttpStatusCode? expectedStatusCode = null) where T : new()
        {
            return _requestExecutor.Execute<T>(request, baseUrl, expectedStatusCode);
        }

        /// <summary>
        /// Synchronous request.  baseUrl is optional and may be assigned if restClient eneds to be anything other than ApiBaseUrl.
        /// Throws a HealthGraphException if response is anything other than OK.
        /// No data is returned.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="baseUrl"></param>
        internal virtual Task Execute(IRestRequest request, string baseUrl = null, HttpStatusCode? expectedStatusCode = null)
        {
            return _requestExecutor.Execute(request, baseUrl, expectedStatusCode);
        }

        /// <summary>
        /// Synchronous request for creation of a resource.  Returns the Location header if present, otherwise returns null.
        /// Throws a HealthGraphException if response is anything other than CREATED.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headerNameOfInterest"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        internal virtual Task<string> ExecuteCreate(IRestRequest request, string baseUrl = null)
        {
            return _requestExecutor.ExecuteCreate(request, baseUrl);
        }

        #endregion
    }
}
