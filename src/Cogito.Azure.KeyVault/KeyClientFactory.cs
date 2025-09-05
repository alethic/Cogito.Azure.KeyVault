using System;

using Azure.Core;
using Azure.Security.KeyVault.Keys;

using Microsoft.Extensions.Options;

namespace Cogito.Azure.KeyVault
{

    /// <summary>
    /// Provides instances of an Azure Key Vault key client.
    /// </summary>
    public class KeyClientFactory
    {

        readonly IOptions<KeyVaultOptions> options;
        readonly TokenCredential? credential;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="credential"></param>
        public KeyClientFactory(IOptions<KeyVaultOptions> options, TokenCredential? credential)
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
