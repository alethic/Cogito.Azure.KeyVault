using System;

using Azure.Core;
using Azure.Security.KeyVault.Secrets;

using AzureKeyVaultEmulator.Aspire.Client;

using Microsoft.Extensions.Options;

namespace Cogito.Azure.KeyVault
{

    /// <summary>
    /// Provides instances of an Azure Key Vault key client.
    /// </summary>
    public class SecretClientFactory
    {

        readonly IOptions<KeyVaultOptions> _options;
        readonly TokenCredential _credential;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="credential"></param>
        public SecretClientFactory(IOptions<KeyVaultOptions> options, TokenCredential credential)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _credential = credential ?? throw new ArgumentNullException(nameof(credential));
        }

        /// <summary>
        /// Creates a new key vault client.
        /// </summary>
        /// <returns></returns>
        public SecretClient CreateSecretClient()
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

            return new SecretClient(_options.Value.VaultUri, credential, new SecretClientOptions()
            {
                DisableChallengeResourceVerification = disableChallengeResourceVerification,
            });
        }

    }

}
