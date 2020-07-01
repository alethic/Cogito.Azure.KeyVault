using System;

using Azure.Security.KeyVault.Secrets;

using Cogito.Autofac;
using Cogito.Components.Azure.Identity;

using Microsoft.Extensions.Options;

namespace Cogito.Components.Azure.KeyVault
{

    /// <summary>
    /// Provides instances of an Azure Key Vault key client.
    /// </summary>
    [RegisterAs(typeof(SecretClientFactory))]
    [RegisterSingleInstance]
    public class SecretClientFactory
    {

        readonly IOptions<KeyVaultOptions> options;
        readonly AzureIdentityCredential credential;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="credential"></param>
        public SecretClientFactory(IOptions<KeyVaultOptions> options, AzureIdentityCredential credential)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.credential = credential ?? throw new ArgumentNullException(nameof(credential));
        }

        /// <summary>
        /// Creates a new key vault client.
        /// </summary>
        /// <returns></returns>
        public SecretClient CreateSecretClient()
        {
            return new SecretClient(options.Value.BaseUri, credential);
        }

    }

}
