
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security;

namespace Grumpydev.Net.Essentials.Core
{
    public class JsonFileKeyVaultAccessInformation : IKeyVaultAccessInformation
    {
        private IConfiguration Configuration;
        private IFileSystem FileSystem;
        private bool _isInformationLoaded;
        private InternalKeyVaultAccessInformation KeyVaultAccessInformation;


        public JsonFileKeyVaultAccessInformation(IConfiguration configuration, IFileSystem fileSystem)
        {
            configuration.ThrowIfNull("The configuration should not be null.");
            fileSystem.ThrowIfNull("You must provide a local file system.");

            this.Configuration = configuration;
            this.FileSystem = fileSystem;
        }

        private InternalKeyVaultAccessInformation LoadInformation()
        {
            var fileName = Configuration.GetSection("appSettings")["SecretFileName"] ?? ".KeyVaultAccessInformation.json";
            var stringConfiguration = System.IO.File.ReadAllText(fileName);
            this.KeyVaultAccessInformation = JsonConvert.DeserializeObject<InternalKeyVaultAccessInformation>(stringConfiguration);
            return this.KeyVaultAccessInformation;
        }
        public string PrincipalApplicationId
        {
            get
            {
                return this.KeyVaultAccessInformation.PrincipalApplicationId ?? this.LoadInformation().PrincipalApplicationId;
            }
        }

        public string LocalCertificateFile
        {
            get
            {
                return this.KeyVaultAccessInformation.LocalCertificateFile ?? this.LoadInformation().LocalCertificateFile;
            }
        }

        public string LocalCertificatePassword
        {
            get
            {
                return this.KeyVaultAccessInformation.LocalCertificatePassword ?? this.LoadInformation().LocalCertificatePassword;
            }
        }

       
        private class InternalKeyVaultAccessInformation : IKeyVaultAccessInformation
        {
            public string PrincipalApplicationId { get; set; }
            
            public string LocalCertificateFile { get; set; }
            
            public string LocalCertificatePassword { get; set; }
        }
    }

   
}
