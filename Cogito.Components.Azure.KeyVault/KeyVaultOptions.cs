using System;

using Cogito.Extensions.Options.Configuration.Autofac;

namespace Cogito.Components.Azure.KeyVault
{

    /// <summary>
    /// Configures the default keyvault.
    /// </summary>
    [RegisterOptions("Azure.KeyVault")]
    public class KeyVaultOptions
    {

        /// <summary>
        /// Base URI of the default system keyvault.
        /// </summary>
        public Uri VaultUri { get; set; }

    }

}
