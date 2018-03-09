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
        
        public IConfiguration Configuration { get; set; }

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

            var applicationId = this.Configuration.GetSection("appSettings")[ConfigurationKeys.AzureKeyVaultApplicationId];

            applicationId.ThrowIfNull($"Missing key in the configuration. The configuration should include a key '{ConfigurationKeys.AzureKeyVaultApplicationId}' containing the AAD application ID to authenticate with. See.."); //TODO: Provide link with help.

            var client = GetKeyVaultClient(certificate, applicationId);

            return client;
        }

        private KeyVaultClient _keyVaultClient;

        public KeyVaultSecretManager(IConfiguration configuration, ICertificateForKeyVaultAuthentication certificateForKeyVaultAuthentication)
        {
            configuration.ThrowIfNull();
            certificateForKeyVaultAuthentication.ThrowIfNull();

            this.Configuration = configuration;
            this.CertificateForKeyVaultAuthentication = certificateForKeyVaultAuthentication;
        }

        public X509Certificate2 GetCertificate(string certificateName)
        {
            throw new NotImplementedException();
        }

        public string GetSecret(string secretName)
        {
            return this.KeyVaultClient.GetSecretAsync(secretName).Result.Value;
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

        public class ConfigurationKeys
        {
            public const string AzureKeyVaultApplicationId = "AzureKeyVaultApplicationId";
        }
    }
}
