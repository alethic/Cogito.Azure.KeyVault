using System;

using Azure;
using Azure.Security.KeyVault.Keys;

namespace Cogito.Azure.KeyVault.DataProtection
{

    /// <summary>
    /// Default implementation of the <see cref="IKeyVaultKeyIdentifierFactory"/> interface.
    /// </summary>
    public class DefaultKeyVaultKeyIdentifierFactory : IKeyVaultKeyIdentifierFactory
    {

        readonly KeyClient _client;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="client"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DefaultKeyVaultKeyIdentifierFactory(KeyClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc />
        public Uri GetIdentifier(string keyName)
        {

            KeyVaultKey? GetKeyOrNull()
            {
                try
                {
                    return _client.GetKey(keyName);
                }
                catch (RequestFailedException e) when (e.Status == 404)
                {
                    return null;
                }
            }

            // get or create new key
            var key = GetKeyOrNull();
            if (key == null)
                key = _client.CreateRsaKey(new CreateRsaKeyOptions(keyName));

            // return the key ID
            return key.Id;
        }

    }

}
