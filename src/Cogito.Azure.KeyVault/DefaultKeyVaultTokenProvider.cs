using System;

using Azure.Core;

namespace Cogito.Azure.KeyVault
{

    /// <summary>
    /// Default implementation of <see cref="IKeyVaultTokenCredentialProvider"/>.
    /// </summary>
    public class DefaultKeyVaultTokenProvider : IKeyVaultTokenCredentialProvider
    {

        readonly TokenCredential _credential;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="credential"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DefaultKeyVaultTokenProvider(TokenCredential credential)
        {
            _credential = credential ?? throw new ArgumentNullException(nameof(credential));
        }

        /// <inheritdoc />
        public TokenCredential GetCredential() => _credential;

    }

}
