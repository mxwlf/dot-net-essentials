using Grumpydev.Net.Essentials.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Grumpydev.Net.Essentials.Web
{
    public class KestrelServerOptionsProxy : IKestrelServerOptionsProxy
    {
        public KestrelServerOptions KestrelServerOptions { get; set; }
        public KestrelServerOptionsProxy(KestrelServerOptions options)
        {
            options.ThrowIfNull();

            this.KestrelServerOptions = options;
        }
        public void ListenToHttpEndpoint(IPAddress ipAddress, int port)
        {
            throw new NotImplementedException();
        }

        public void ListenToHttpsEndpoint(IPAddress ipAddress, int port, X509Certificate2 tlsCertificate)
        {
            throw new NotImplementedException();
        }
    }
}
