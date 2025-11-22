using System;

using Azure.Core;
using Azure.Security.KeyVault.Certificates;

using AzureKeyVaultEmulator.Aspire.Client;

using Microsoft.Extensions.Options;

namespace Cogito.Azure.KeyVault
{

    /// <summary>
    /// Provides instances of an Azure Key Vault certificate client.
    /// </summary>
    public class CertificateClientFactory
    {

        readonly IOptions<KeyVaultOptions> _options;
        readonly TokenCredential _credential;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="credential"></param>
        public CertificateClientFactory(IOptions<KeyVaultOptions> options, TokenCredential credential)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _credential = credential ?? throw new ArgumentNullException(nameof(credential));
        }

        /// <summary>
        /// Creates a new key vault certificate client.
        /// </summary>
        /// <returns></returns>
        public CertificateClient CreateSecretClient()
        {
            if (_options.Value.VaultUri is null)
                throw new InvalidOperationException("VaultUri has not been configured.");

            var credential = _credential;
            var disableChallengeResourceVerification = _options.Value.DisableChallengeResourceVerification ?? false;
            if (_options.Value.UseEmulator == true)
            {
                credential = new EmulatedTokenCredential(_options.Value.VaultUri.ToString());
                disableChallengeResourceVerification = true;
            }

            return new CertificateClient(_options.Value.VaultUri, credential, new CertificateClientOptions()
            {
                DisableChallengeResourceVerification = disableChallengeResourceVerification,
            });
        }

    }

}
