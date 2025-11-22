using Azure.Core;

namespace Cogito.Azure.KeyVault
{

    /// <summary>
    /// Provides the <see cref="TokenCredential"/> to the Key Vault clients.
    /// </summary>
    public interface IKeyVaultTokenCredentialProvider
    {

        TokenCredential GetCredential();

    }

}