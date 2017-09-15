﻿using System;
using System.Threading.Tasks;
using System.Diagnostics;
using HealthGraphNet.RestSharp;
using RestSharp.Portable.OAuth2.Infrastructure;

namespace HealthGraphNet.Samples.Web
{
    /// <summary>
    /// Authenticator for web based apps
    /// </summary>
    public class WebAuthenticator : StaticAuthenticator
    {
        private HealthGraphClient _client;
        public HealthGraphClient Client
        {
            get
            {
                return _client;
            }
        }

        public WebAuthenticator(HealthGraphClient client)
        {
            _client = client;
        }

        public async Task OnPageLoaded(Uri uri)
        {
            try
            {
                if (uri.AbsoluteUri.StartsWith(_client.Configuration.RedirectUri))
                {
                    Debug.WriteLine("Navigated to redirect url.");
                    if (uri.Query.IsEmpty())
                        return;

                    var parameters = uri.Query.Remove(0, 1).ParseQueryString(); // query portion of the response
                    await _client.GetUserInfo(parameters);

                    if (!string.IsNullOrEmpty(_client.AccessToken))
                    {
                        AccessToken = _client.AccessToken;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
