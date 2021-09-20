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
        private readonly IHost Host;
        private readonly TestServer _server;


        public HttpClient Client { get; }
        public Request Request { get; }

        public IntegrationFixture()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            var hostBuilder = new HostBuilder()
                        .ConfigureWebHost(webHost =>
                        {
                            webHost.UseTestServer();
                            webHost.UseStartup<Startup>();
                        })
                        .UseServiceProviderFactory(new AutofacServiceProviderFactory());

            Host = hostBuilder.Start();
            _server = Host.GetTestServer();
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
