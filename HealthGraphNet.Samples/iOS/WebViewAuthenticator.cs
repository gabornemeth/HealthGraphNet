using RestSharp.Portable.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp.Portable;
using System.Net;
using System.Threading.Tasks;
using HealthGraphNet.RestSharp;
using RestSharp.Portable.OAuth2;

namespace HealthGraphNet.Samples.MonoTouch
{
    class WebViewAuthenticator : OAuth2Authenticator
    {
        private HealthGraphClient _client;
        public new HealthGraphClient Client => _client;

        public WebViewAuthenticator(HealthGraphClient client) : base(client)
        {
            _client = client;
        }

        public override bool CanPreAuthenticate(IHttpClient client, IHttpRequestMessage request, ICredentials credentials)
        {
            throw new NotImplementedException();
        }

        public override bool CanPreAuthenticate(IRestClient client, IRestRequest request, ICredentials credentials)
        {
            throw new NotImplementedException();
        }

        public override Task PreAuthenticate(IHttpClient client, IHttpRequestMessage request, ICredentials credentials)
        {
            throw new NotImplementedException();
        }

        public override Task PreAuthenticate(IRestClient client, IRestRequest request, ICredentials credentials)
        {
            throw new NotImplementedException();
        }
    }
}
