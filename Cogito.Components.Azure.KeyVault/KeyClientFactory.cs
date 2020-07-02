using System;

using Azure.Security.KeyVault.Keys;

using Cogito.Autofac;
using Cogito.Components.Azure.Identity;

using Microsoft.Extensions.Options;

namespace Cogito.Components.Azure.KeyVault
{

    /// <summary>
    /// Provides instances of an Azure Key Vault key client.
    /// </summary>
    [RegisterAs(typeof(KeyClientFactory))]
    [RegisterSingleInstance]
    public class KeyClientFactory
    {

        readonly IOptions<KeyVaultOptions> options;
        readonly AzureIdentityCredential credential;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="credential"></param>
        public KeyClientFactory(IOptions<KeyVaultOptions> options, AzureIdentityCredential credential)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.credential = credential ?? throw new ArgumentNullException(nameof(credential));
        }

        /// <summary>
        /// Creates a new key vault client.
        /// </summary>
        /// <returns></returns>
        public KeyClient CreateKeyClient()
        {
            return new KeyClient(options.Value.VaultUri, credential);
        }

    }

}
