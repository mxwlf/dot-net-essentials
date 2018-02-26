using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Grumpydev.Net.Essentials.Core
{
    public interface ICertificateProvider
    {
        X509Certificate2 GetCertificate(string certificateName);
        
    }
}
