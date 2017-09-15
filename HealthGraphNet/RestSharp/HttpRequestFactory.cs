using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable.OAuth2.Infrastructure;

namespace HealthGraphNet.Samples.Web
{
    public class HttpRequestFactory : IRequestFactory
    {
        public IRestClient CreateClient()
        {
            return new RestClient();
        }

        public IRestRequest CreateRequest(string resource)
        {
            return new RestRequest(resource);
        }
    }
}