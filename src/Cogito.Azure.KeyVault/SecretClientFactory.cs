using System;

using Azure.Security.KeyVault.Secrets;

using Microsoft.Extensions.Options;

namespace Cogito.Azure.KeyVault
{

    /// <summary>
    /// Provides instances of an Azure Key Vault key client.
    /// </summary>
    public class SecretClientFactory
    {

        readonly IOptions<KeyVaultOptions> _options;
        readonly IKeyVaultTokenCredentialProvider _credentialProvider;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="credentialProvider"></param>
        public SecretClientFactory(IOptions<KeyVaultOptions> options, IKeyVaultTokenCredentialProvider credentialProvider)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _credentialProvider = credentialProvider ?? throw new ArgumentNullException(nameof(credentialProvider));
        }

        /// <summary>
        /// Creates a new key vault client.
        /// </summary>
        /// <returns></returns>
        public SecretClient CreateSecretClient()
        {
            if (_options.Value.VaultUri is null)
                throw new InvalidOperationException("VaultUri has not been configured.");

            return new SecretClient(_options.Value.VaultUri, _credentialProvider.GetCredential(), new SecretClientOptions()
            {
                DisableChallengeResourceVerification = _options.Value.DisableChallengeResourceVerification ?? false,
            });
        }

    }

}
