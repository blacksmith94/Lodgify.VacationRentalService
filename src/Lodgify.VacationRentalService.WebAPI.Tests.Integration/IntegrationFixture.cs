using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using Xunit;

namespace Lodgify.VacationRentalService.WebAPI.Tests.Integration
{
    [CollectionDefinition("Integration")]
    public sealed class IntegrationFixture : IDisposable, ICollectionFixture<IntegrationFixture>
    {
        private readonly IHost _host;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public Request Request { get; }

        public IntegrationFixture()
        {
            var hostBuilder = new HostBuilder()
                        .ConfigureWebHost(webHost =>
                        {
                            webHost.UseTestServer();
                            webHost.UseStartup<Startup>();
                        })
                        .UseServiceProviderFactory(new AutofacServiceProviderFactory());

            _host = hostBuilder.Start();
            _server = _host.GetTestServer();
            _client = _server.CreateClient();

            Request = new Request(_client);
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}
