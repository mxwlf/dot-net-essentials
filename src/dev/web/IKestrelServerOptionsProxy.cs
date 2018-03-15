using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Grumpydev.Net.Essentials.Web
{
    public interface IKestrelServerOptionsProxy
    {
        void ListenToHttpEndpoint(IPAddress ipAddress, int port);

        void ListenToHttpsEndpoint(IPAddress ipAddress, int port, X509Certificate2 tlsCertificate);

    }
}
