using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

using Azure.Security.KeyVault.Certificates;
using Azure.Security.KeyVault.Secrets;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Cogito.Azure.KeyVault
{

    /// <summary>
    /// Provides X509 Certificates from Azure KeyVault.
    /// </summary>
    public class KeyVaultX509Certificate2Provider
    {

#if NETSTANDARD2_0 || NET462
        const X509KeyStorageFlags DEFAULT_FLAGS = X509KeyStorageFlags.DefaultKeySet;
#else
        const X509KeyStorageFlags DEFAULT_FLAGS = X509KeyStorageFlags.EphemeralKeySet;
#endif

        readonly CertificateClient certificateClient;
        readonly SecretClient secretClient;
        readonly ILogger logger;
        readonly IMemoryCache cache;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="certificateClient"></param>
        /// <param name="secretClient"></param>
        /// <param name="logger"></param>
        /// <param name="cache"></param>
        public KeyVaultX509Certificate2Provider(CertificateClient certificateClient, SecretClient secretClient, ILogger<KeyVaultX509Certificate2Provider> logger, IMemoryCache cache = null)
        {
            this.certificateClient = certificateClient ?? throw new ArgumentNullException(nameof(certificateClient));
            this.secretClient = secretClient ?? throw new ArgumentNullException(nameof(secretClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.cache = cache ?? new MemoryCache(new MemoryCacheOptions() { });
        }

        /// <summary>
        /// Downloads the certificate from the key vault store.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="flags"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        async Task<X509Certificate2> LoadCertifcateAsync(string name, X509KeyStorageFlags flags, CancellationToken cancellationToken)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            // download certificate from key vault
            logger.LogInformation("Loading certificate {CertificateName}.", name);
            var crt = await certificateClient.GetCertificateAsync(name, cancellationToken);
            if (crt.Value == null)
                throw new InvalidOperationException($"Unable to load Key Vault certificate '{name}'.");

            var sid = new KeyVaultSecretIdentifier(crt.Value.SecretId);
            if (sid.VaultUri != certificateClient.VaultUri)
                throw new Exception("Certificate secret not from the same vault as the certificate.");

            logger.LogInformation("Loading secret {SecretId}.", sid);
            var key = await secretClient.GetSecretAsync(sid.Name, cancellationToken: cancellationToken);
            if (key.Value == null)
                throw new InvalidOperationException($"Unable to load Key Vault secret from '{sid}'.");

            // load private key
            var pfx = new X509Certificate2(
                Convert.FromBase64String(key.Value.Value),
                (string)null,
                flags);

            // log certificate information
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Loaded certificate from {Id}: {@Certificate}.", crt.Value.Id, new
                {
                    pfx.FriendlyName,
                    pfx.Issuer,
                    pfx.SerialNumber,
                    pfx.Subject,
                    pfx.Thumbprint
                });

            // return first certificate in collection
            return pfx;
        }

        /// <summary>
        /// Gets the X509 Certificate with the given ID.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="flags"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<X509Certificate2> GetX509Certificate2Async(string name, X509KeyStorageFlags flags = DEFAULT_FLAGS, CancellationToken cancellationToken = default)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            return cache.GetOrCreateAsync((typeof(KeyVaultX509Certificate2Provider), name), _ => LoadCertifcateAsync(name, flags, cancellationToken));
        }

    }

}
