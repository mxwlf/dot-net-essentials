using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Grumpydev.Net.Essentials.Web.ConfigurationModels;
using Grumpydev.Net.Essentials.Core;


namespace Grumpydev.Net.Essentials.Web
{
    public static class KestrelServerOptionsExtensions
    {
        public static void ConfigureEndpoints(this KestrelServerOptions options, IConfiguration configuration, X509Certificate2 tlsCertificate = null, IKestrelServerOptionsProxy kestrelServerOptionsProxy = null)
        {
            kestrelServerOptionsProxy = kestrelServerOptionsProxy ?? new KestrelServerOptionsProxy(options);

            var endpoints = configuration.GetSection("HttpServer:Endpoints")
                .GetChildren()
                .ToDictionary(section => section.Key, section =>
                {
                    var endpoint = new EndpointConfiguration();
                    section.Bind(endpoint);
                    return endpoint;
                });

            foreach (var endpoint in endpoints)
            {
                var config = endpoint.Value;
                var port = config.Port;

                var ipAddresses = new System.Collections.Generic.List<IPAddress>();
                if (config.Host == "localhost")
                {
                    ipAddresses.Add(IPAddress.IPv6Loopback);
                    ipAddresses.Add(IPAddress.Loopback);
                }
                else if (IPAddress.TryParse(config.Host, out var address))
                {
                    ipAddresses.Add(address);
                }
                else
                {
                    ipAddresses.Add(IPAddress.IPv6Any);
                }

                foreach (var address in ipAddresses)
                {
                    if (config.Scheme == "https")
                    {
                        kestrelServerOptionsProxy.ListenToHttpsEndpoint(address, port, tlsCertificate);
                    }
                    else
                    {
                        kestrelServerOptionsProxy.ListenToHttpEndpoint(address, port);
                    }
                }
            }
        }
    }
}

