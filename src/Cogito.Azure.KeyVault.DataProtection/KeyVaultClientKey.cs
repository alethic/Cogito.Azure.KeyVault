using System;
using System.Threading;
using System.Threading.Tasks;

using Azure.Core.Cryptography;
using Azure.Security.KeyVault.Keys;

namespace Cogito.Azure.KeyVault.DataProtection
{

    public class KeyVaultClientKey : IKeyEncryptionKey
    {

        readonly KeyClient _client;
        readonly KeyVaultKey _key;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="key"></param>
        public KeyVaultClientKey(KeyClient client, KeyVaultKey key)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public string KeyId => _key.Id.ToString();

        public byte[] UnwrapKey(string algorithm, ReadOnlyMemory<byte> encryptedKey, CancellationToken cancellationToken = default)
        {
            _client.
        }

        public Task<byte[]> UnwrapKeyAsync(string algorithm, ReadOnlyMemory<byte> encryptedKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public byte[] WrapKey(string algorithm, ReadOnlyMemory<byte> key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> WrapKeyAsync(string algorithm, ReadOnlyMemory<byte> key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

    }

}
