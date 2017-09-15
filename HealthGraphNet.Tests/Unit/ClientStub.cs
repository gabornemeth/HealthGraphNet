using System;
using System.Net;
using RestSharp.Portable;
using System.Threading.Tasks;

namespace HealthGraphNet.Tests.Unit
{
    /// <summary>
    /// Stubbing out the actual restsharp request execution. Overriding internals in a different assembly thanks to the InternalsVisibleTo attribute.
    /// </summary>
    public class ClientStub : Client
    {
        public ClientStub() : base(null, new StubRequestExecutor())
        {
        }

        private class StubRequestExecutor : IRequestExecutor
        {
            public Task<T> Execute<T>(IRestRequest request, string baseUrl = null, HttpStatusCode? expectedStatusCode = null) where T : new()
            {
                return Task.FromResult(new T());
            }

            public Task Execute(IRestRequest request, string baseUrl = null, HttpStatusCode? expectedStatusCode = null)
            {
                return Task.Run(() => { });
            }

            public Task<string> ExecuteCreate(IRestRequest request, string baseUrl = null)
            {
                return Task.FromResult(string.Empty);
            }

        }
    }
}