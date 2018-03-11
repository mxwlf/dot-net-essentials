using System.Security;

namespace Grumpydev.Net.Essentials.Core
{
    /// <summary>
    /// Interface for methods to extract the information to access KeyVault.
    /// </summary>
    public interface IKeyVaultAccessInformation
    {
        string PrincipalApplicationId { get; }

        string LocalCertificateFile { get; }

        //TODO: Convert to Securestring
        string LocalCertificatePassword { get; }
        
        string KeyVaultUrl { get; }
    }
}
