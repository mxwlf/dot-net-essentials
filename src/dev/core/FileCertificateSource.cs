using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Grumpydev.Net.Essentials.Core
{
    public class FileCertificateSource : ICertificateForKeyVaultAuthentication
    {
        public IKeyVaultAccessInformation KeyVaultAccessInformation { get; set; }

        public FileCertificateSource(IKeyVaultAccessInformation keyVaultAccessInformation)
        {
            keyVaultAccessInformation.ThrowIfNull("The keyVaultAccessInformation parameter must be non null.");

            this.KeyVaultAccessInformation = keyVaultAccessInformation;
        }

        public X509Certificate2 GetCertificateForAuthentication()
        {
            var cert =  new X509Certificate2(this.KeyVaultAccessInformation.LocalCertificateFile, this.KeyVaultAccessInformation.LocalCertificatePassword, X509KeyStorageFlags.DefaultKeySet);
            return cert;
        }
    }
}
