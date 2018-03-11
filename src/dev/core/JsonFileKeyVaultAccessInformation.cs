using Newtonsoft.Json;

namespace Grumpydev.Net.Essentials.Core
{
    public class JsonFileKeyVaultAccessInformation : IKeyVaultAccessInformation
    {
        private IFileSystem FileSystem;

        private InternalKeyVaultAccessInformation KeyVaultAccessInformation;


        public JsonFileKeyVaultAccessInformation(IFileSystem fileSystem)
        {
            fileSystem.ThrowIfNull("You must provide a local file system.");
            this.FileSystem = fileSystem;
        }

        private InternalKeyVaultAccessInformation LoadInformation()
        {
            var fileName =  ".KeyVaultAccessInformation.json"; //TODO: override with configuration?

            var stringConfiguration = System.IO.File.ReadAllText(fileName);
            this.KeyVaultAccessInformation = JsonConvert.DeserializeObject<InternalKeyVaultAccessInformation>(stringConfiguration);
            return this.KeyVaultAccessInformation;
        }
        public string PrincipalApplicationId
        {
            get
            {
                return this.KeyVaultAccessInformation?.PrincipalApplicationId ?? this.LoadInformation().PrincipalApplicationId;
            }
        }

        public string LocalCertificateFile
        {
            get
            {
                return this.KeyVaultAccessInformation?.LocalCertificateFile ?? this.LoadInformation().LocalCertificateFile;
            }
        }

        public string LocalCertificatePassword
        {
            get
            {
                return this.KeyVaultAccessInformation?.LocalCertificatePassword ?? this.LoadInformation().LocalCertificatePassword;
            }
        }

        public string KeyVaultUrl
        {
            get
            {
                var url = this.KeyVaultAccessInformation?.KeyVaultUrl ?? this.LoadInformation().KeyVaultUrl;

                if (!string.IsNullOrEmpty(url))
                {
                    url = url.TrimEnd('/');
                }

                return url;
            }
        }

        private class InternalKeyVaultAccessInformation : IKeyVaultAccessInformation
        {
            public string PrincipalApplicationId { get; set; }
            
            public string LocalCertificateFile { get; set; }
            
            public string LocalCertificatePassword { get; set; }

            public string KeyVaultUrl { get; set; }
        }
    }

   
}
