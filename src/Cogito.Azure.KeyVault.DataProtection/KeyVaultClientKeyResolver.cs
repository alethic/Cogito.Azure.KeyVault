using System;
using System.Threading;
using System.Threading.Tasks;

using Azure.Core.Cryptography;
using Azure.Security.KeyVault.Keys;

namespace Cogito.Azure.KeyVault.DataProtection
{

    public class KeyVaultClientKeyResolver : IKeyEncryptionKeyResolver
    {

        readonly KeyClient _client;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="client"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public KeyVaultClientKeyResolver(KeyClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc />
        public IKeyEncryptionKey Resolve(string keyId, CancellationToken cancellationToken = default)
        {
            if (keyId is null)
                throw new ArgumentNullException(nameof(keyId));

            var key = _client.GetKey(keyId, cancellationToken: cancellationToken);

            return new KeyVaultClientKey(_client, key);

        }

        /// <inheritdoc />
        public async Task<IKeyEncryptionKey> ResolveAsync(string keyId, CancellationToken cancellationToken = default)
        {
            if (keyId is null)
                throw new ArgumentNullException(nameof(keyId));

            var key = await _client.GetKeyAsync(keyId, cancellationToken: cancellationToken);

            return new KeyVaultClientKey(_client, key);
        }

    }

}
