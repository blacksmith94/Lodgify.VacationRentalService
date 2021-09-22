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

        public HttpClient Client { get; }
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

            Client = _server.CreateClient();
            Request = new Request(Client);
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
