using Grumpydev.Net.Essentials.Azure;
using Grumpydev.Net.Essentials.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;


namespace azure_tests_integration
{
    [TestClass]
    public class KeyVaultTests
    {
        [TestMethod]
        public void GetSecret_WhenCalledCorrectly_ShouldReturnSecret()
        {
            // Arrange.
            var keyVaultAccessInformation = new JsonFileKeyVaultAccessInformation(new LocalFileSystem());
            var certficateStore = new FileCertificateSource(keyVaultAccessInformation);
            var keyvault = new KeyVaultSecretManager(keyVaultAccessInformation, certficateStore);

            // Act.
            var secret = keyvault.GetSecret("TestSecret"); //TODO: Remove the long URL

            // Assert
            secret.Should().NotBeNull(); // Get a previously saved secret.
        }

        [TestMethod]
        public void GetCertificate()
        {
            // Arrange.
            var keyVaultAccessInformation = new JsonFileKeyVaultAccessInformation(new LocalFileSystem());
            var certficateStore = new FileCertificateSource(keyVaultAccessInformation);
            var keyvault = new KeyVaultSecretManager(keyVaultAccessInformation, certficateStore);

            var certificate = keyvault.GetCertificate("TestSelfSignedCert");

            certificate.Should().NotBeNull();



        }
    }
}
