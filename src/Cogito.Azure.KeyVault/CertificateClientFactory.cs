using System;

using Azure.Security.KeyVault.Certificates;

using Cogito.Azure.Identity;

using Microsoft.Extensions.Options;

namespace Cogito.Azure.KeyVault
{

    /// <summary>
    /// Provides instances of an Azure Key Vault certificate client.
    /// </summary>
    public class CertificateClientFactory
    {

        readonly IOptions<KeyVaultOptions> options;
        readonly AzureIdentityCredential credential;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="credential"></param>
        public CertificateClientFactory(IOptions<KeyVaultOptions> options, AzureIdentityCredential credential)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.credential = credential ?? throw new ArgumentNullException(nameof(credential));
        }

        /// <summary>
        /// Creates a new key vault certificate client.
        /// </summary>
        /// <returns></returns>
        public CertificateClient CreateSecretClient()
        {
            return new CertificateClient(options.Value.VaultUri, credential);
        }

    }

}
