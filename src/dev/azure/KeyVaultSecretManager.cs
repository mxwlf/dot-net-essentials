using Grumpydev.Net.Essentials.Core;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using static Microsoft.Azure.KeyVault.KeyVaultClient;

namespace Grumpydev.Net.Essentials.Azure
{
    public class KeyVaultSecretManager : ICertificateProvider
    {
        public IKeyVaultAccessInformation KeyVaultAccessInformation { get; set; }
        public ICertificateForKeyVaultAuthentication CertificateForKeyVaultAuthentication { get; set; }

        internal KeyVaultClient KeyVaultClient
        {
            get
            {
                return _keyVaultClient ?? this.GetKeyVaultClient();
            }
            set
            {
                _keyVaultClient = value;
            }
        }

        internal KeyVaultClient GetKeyVaultClient()
        {
            var certificate = this.CertificateForKeyVaultAuthentication.GetCertificateForAuthentication();
            var applicationId = this.KeyVaultAccessInformation.PrincipalApplicationId;
           
            var client = GetKeyVaultClient(certificate, applicationId);

            return client;
        }

        private KeyVaultClient _keyVaultClient;

        public KeyVaultSecretManager(IKeyVaultAccessInformation keyVaultAccessInformation, ICertificateForKeyVaultAuthentication certificateForKeyVaultAuthentication)
        {
            keyVaultAccessInformation.ThrowIfNull();
            certificateForKeyVaultAuthentication.ThrowIfNull();

            this.CertificateForKeyVaultAuthentication = certificateForKeyVaultAuthentication;
            this.KeyVaultAccessInformation = keyVaultAccessInformation;

        }

        public X509Certificate2 GetCertificate(string certificateName)
        {
            var certificateText = this.GetSecret(certificateName);
            var certBytes = Convert.FromBase64String(certificateText);
            var certificate = new X509Certificate2(certBytes);
            return certificate;
        }


        public string GetSecret(string secretName)
        {
            var secretUrl = BuildSecretUrl(secretName, this.KeyVaultAccessInformation.KeyVaultUrl);

            return this.KeyVaultClient.GetSecretAsync(secretUrl).Result.Value;
        }

        internal static string BuildSecretUrl(string secretName, string keyVaultUrl)
        {
            return $"{keyVaultUrl}/secrets/{secretName}/";
        }

        internal static KeyVaultClient GetKeyVaultClient(X509Certificate2 certificate, string applicationId, HttpClient httpClient = null)
        {
            httpClient = httpClient ?? new HttpClient();

            var keyVaultClient = new KeyVaultClient(new AuthenticationCallback(async
                (authority, resource, scope) =>
                {
                    var authContext = new AuthenticationContext(authority, TokenCache.DefaultShared);

                    var result = await authContext.AcquireTokenAsync(resource, new ClientAssertionCertificate(applicationId, certificate));
                    return result.AccessToken;
                }
            ), httpClient);

            return keyVaultClient;
        }
    }
}
