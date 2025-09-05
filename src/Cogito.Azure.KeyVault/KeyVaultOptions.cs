using System;

namespace Cogito.Azure.KeyVault
{

    /// <summary>
    /// Configures the default keyvault.
    /// </summary>
    public class KeyVaultOptions
    {

        /// <summary>
        /// Base URI of the default system keyvault.
        /// </summary>
        public Uri? VaultUri { get; set; }

    }

}
