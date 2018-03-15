using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using Grumpydev.Net.Essentials.Web;
using System.Net;
using System;
using FluentAssertions;
using System.Security.Cryptography.X509Certificates;

namespace Grumpydev.Net.Essentials.Web.Tests
{
    [TestClass]
    public class KestrelServerOptionsExtensionsTests
    {
        [TestMethod]
        public void ConfigureEndpoints_WhenOnlyHttpEndpointIsConfigured_ShouldListenForHttpOnly()
        {

            var options = Substitute.For<KestrelServerOptions>();
            var kestrelServerOptionsProxy = Substitute.For<IKestrelServerOptionsProxy>();

            var endpoints = new Dictionary<string, ConfigurationModels.EndpointConfiguration>()
            {
                { "http", new ConfigurationModels.EndpointConfiguration() { Host = "www.example.com", Port = 80, Scheme = "http" } }
            };

            var configuration = GetMockConfigurationFromEndpoints(endpoints);
            
            KestrelServerOptionsExtensions.ConfigureEndpoints(options, configuration, kestrelServerOptionsProxy : kestrelServerOptionsProxy);

            kestrelServerOptionsProxy.Received().ListenToHttpEndpoint(IPAddress.IPv6Any, 80);
            
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

    }
}
