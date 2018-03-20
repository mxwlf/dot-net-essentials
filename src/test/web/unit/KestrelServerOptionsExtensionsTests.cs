using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;



namespace Grumpydev.Net.Essentials.Web.Tests
{
    [TestClass]
    public class KestrelServerOptionsExtensionsTests
    {
        [TestMethod]
        public void ConfigureEndpoints_WhenOnlyHttpEndpointIsConfigured_ShouldListenForHttp()
        {
            // Arrange.
            var options = Substitute.For<KestrelServerOptions>();
            var kestrelServerOptionsProxy = Substitute.For<IKestrelServerOptionsProxy>();

            var endpoints = new Dictionary<string, ConfigurationModels.EndpointConfiguration>()
            {
                { "http", new ConfigurationModels.EndpointConfiguration() { Host = "www.example.com", Port = 80, Scheme = "http" } }
            };

            var configuration = GetMockConfigurationFromEndpoints(endpoints);
            
            // Act.
            KestrelServerOptionsExtensions.ConfigureEndpoints(options, configuration, kestrelServerOptionsProxy : kestrelServerOptionsProxy);

            // Assert.
            kestrelServerOptionsProxy.Received().ListenToHttpEndpoint(IPAddress.IPv6Any, 80);
            
        }

        [TestMethod]
        public void ConfigureEndpoints_WhenOnlyHttpEndpointIsConfigured_ShouldNotListenForHttps()
        {
            // Arrange.
            var options = Substitute.For<KestrelServerOptions>();
            var kestrelServerOptionsProxy = Substitute.For<IKestrelServerOptionsProxy>();

            var endpoints = new Dictionary<string, ConfigurationModels.EndpointConfiguration>()
            {
                { "http", new ConfigurationModels.EndpointConfiguration() { Host = "www.example.com", Port = 80, Scheme = "http" } }
            };

            var configuration = GetMockConfigurationFromEndpoints(endpoints);

            // Act.
            KestrelServerOptionsExtensions.ConfigureEndpoints(options, configuration, kestrelServerOptionsProxy: kestrelServerOptionsProxy);

            // Assert.
            kestrelServerOptionsProxy.DidNotReceiveWithAnyArgs().ListenToHttpsEndpoint(Arg.Any<IPAddress>(), Arg.Any<int>(), Arg.Any<X509Certificate2>());

        }

        [TestMethod]
        public void ConfigureEndpoints_WhenOnlyHttpsEndpointIsConfigured_ShouldListenForHttps()
        {
            // Arrange.
            var options = Substitute.For<KestrelServerOptions>();
            var kestrelServerOptionsProxy = Substitute.For<IKestrelServerOptionsProxy>();
            var certificate = GetMockCertificate();


            var endpoints = new Dictionary<string, ConfigurationModels.EndpointConfiguration>()
            {
                { "https", new ConfigurationModels.EndpointConfiguration() { Host = "www.example.com", Port = 443, Scheme = "https" } }
            };

            var configuration = GetMockConfigurationFromEndpoints(endpoints);

            // Act.
            KestrelServerOptionsExtensions.ConfigureEndpoints(options, configuration, certificate, kestrelServerOptionsProxy);

            // Assert.
            kestrelServerOptionsProxy.Received().ListenToHttpsEndpoint(IPAddress.IPv6Any, 443, Arg.Is<X509Certificate2>( x => x.Equals(certificate) ));

        }

        [TestMethod]
        public void ConfigureEndpoints_WhenOnlyHttpsEndpointIsConfigured_ShouldNotListenForHttp()
        {
            // Arrange.
            var options = Substitute.For<KestrelServerOptions>();
            var kestrelServerOptionsProxy = Substitute.For<IKestrelServerOptionsProxy>();
            var certificate = GetMockCertificate();

            var endpoints = new Dictionary<string, ConfigurationModels.EndpointConfiguration>()
            {
                { "https", new ConfigurationModels.EndpointConfiguration() { Host = "www.example.com", Port = 443, Scheme = "https" } }
            };

            var configuration = GetMockConfigurationFromEndpoints(endpoints);

            // Act.
            KestrelServerOptionsExtensions.ConfigureEndpoints(options, configuration, certificate, kestrelServerOptionsProxy);

            // Assert.
            kestrelServerOptionsProxy.DidNotReceiveWithAnyArgs().ListenToHttpEndpoint(IPAddress.IPv6Any, Arg.Any<int>());

        }

        [TestMethod]
        public void ConfigureEndpoints_BothHttpAndHttpsAreConfigured_ShouldListenToHttpAndHttpsPorts()
        {
            // Arrange.
            var options = Substitute.For<KestrelServerOptions>();
            var kestrelServerOptionsProxy = Substitute.For<IKestrelServerOptionsProxy>();
            var certificate = GetMockCertificate();

            var endpoints = new Dictionary<string, ConfigurationModels.EndpointConfiguration>()
            {
                { "https", new ConfigurationModels.EndpointConfiguration() { Host = "www.example.com", Port = 443, Scheme = "https" } },
                { "http", new ConfigurationModels.EndpointConfiguration() { Host = "www.example.com", Port = 82, Scheme = "http" } }
            };

            var configuration = GetMockConfigurationFromEndpoints(endpoints);

            // Act.
            KestrelServerOptionsExtensions.ConfigureEndpoints(options, configuration, certificate, kestrelServerOptionsProxy);

            // Assert.
            kestrelServerOptionsProxy.Received().ListenToHttpsEndpoint(IPAddress.IPv6Any, 443, certificate);
            kestrelServerOptionsProxy.Received().ListenToHttpEndpoint(IPAddress.IPv6Any, 82);


        }


        public IConfiguration GetMockConfigurationFromEndpoints(IDictionary<string,ConfigurationModels.EndpointConfiguration> endpoints)
        {
            var configuration = new ConfigurationBuilder();

            var list = new List<KeyValuePair<string, string>>();

            foreach (var endpoint in endpoints)
            {
                list.Add(new KeyValuePair<string, string>($"HttpServer:Endpoints:{endpoint.Key}:Scheme", endpoint.Value.Scheme));
                list.Add(new KeyValuePair<string, string>($"HttpServer:Endpoints:{endpoint.Key}:Port", endpoint.Value.Port.ToString()));
                list.Add(new KeyValuePair<string, string>($"HttpServer:Endpoints:{endpoint.Key}:Host", endpoint.Value.Host));
            }

            configuration.AddInMemoryCollection(list);
            return configuration.Build();
        }

        private X509Certificate2 GetMockCertificate()
        {
            return new X509Certificate2(); // TODO: Generate a true mock certificate
        }


    }
}
