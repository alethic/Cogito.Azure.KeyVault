using System;

using Azure.Core;

using AzureKeyVaultEmulator.Aspire.Client;

using Microsoft.Extensions.Options;

namespace Cogito.Azure.KeyVault
{

    /// <summary>
    /// Default implementation of <see cref="IKeyVaultTokenCredentialProvider"/>.
    /// </summary>
    public class DefaultKeyVaultTokenCredentialProvider : IKeyVaultTokenCredentialProvider
    {

        readonly IOptions<KeyVaultOptions> _options;
        readonly TokenCredential _credential;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="credential"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DefaultKeyVaultTokenCredentialProvider(IOptions<KeyVaultOptions> options, TokenCredential credential)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _credential = credential ?? throw new ArgumentNullException(nameof(credential));
        }

        /// <inheritdoc />
        public TokenCredential GetCredential()
        {
            if (_options.Value.UseEmulator == true)
            {
                if (_options.Value.VaultUri is null)
                    throw new InvalidOperationException("VaultUri has not been configured.");

                return new EmulatedTokenCredential(_options.Value.VaultUri.AbsoluteUri);
            }

            return _credential;
        }

    }

}
