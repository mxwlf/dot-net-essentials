using System;
using System.IO;
using FluentAssertions;
using Grumpydev.Net.Essentials.Azure;
using Grumpydev.Net.Essentials.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace azure_tests_integration
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var builder = new ConfigurationBuilder();
            var jsonSource = new JsonConfigurationSource();
            jsonSource.Path =  "appsettings.json";
            builder.Add(jsonSource);

            var configuration = builder.Build();

            var keyvault = new KeyVaultSecretManager(configuration, new FileCertificateSource(new JsonFileKeyVaultAccessInformation(configuration, new LocalFileSystem())));

            var secret =  keyvault.GetSecret("https://grumpydevinttestkeyvault.vault.azure.net/secrets/TestSecret/");

            secret.Should().NotBeNull();


        }

    }
}
