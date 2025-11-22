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

        /// <summary>
        /// Gets or sets whether to disable verification that the authentication challenge resource matches the Key Vault domain.
        /// </summary>
        public bool? DisableChallengeResourceVerification { get; set; }

    }

}
