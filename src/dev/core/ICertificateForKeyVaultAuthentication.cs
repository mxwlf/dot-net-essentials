using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Grumpydev.Net.Essentials.Core
{
    /// <summary>
    /// Gets a certificate to be used to authenticate Azure KeyVault.
    /// </summary>
    /// <remarks>
    ///  This may sound redundant but the prefered way to obtain a certificate is from Azure KeyVault.
    ///  To authenticate into Azure KeyVault the prefered way is to use a certificate. 
    ///  So to avoid a circular dependency during composition, I separate this interface to be used by the Azure Keyvault client to gain access.
    /// </remarks>
    public interface ICertificateForKeyVaultAuthentication
    {
        X509Certificate2 GetCertificateForAuthentication();
    }
}
