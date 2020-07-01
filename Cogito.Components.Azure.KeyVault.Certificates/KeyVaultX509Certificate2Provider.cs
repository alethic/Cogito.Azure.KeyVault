using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

using Azure.Security.KeyVault.Certificates;
using Azure.Security.KeyVault.Secrets;

using Cogito.Autofac;

using Microsoft.Extensions.Caching.Memory;

using Serilog;
using Serilog.Events;

namespace Cogito.Components.Azure.KeyVault.Certificates
{

    /// <summary>
    /// Provides X509 Certificates from Azure KeyVault.
    /// </summary>
    [RegisterAs(typeof(KeyVaultX509Certificate2Provider))]
    [RegisterSingleInstance]
    public class KeyVaultX509Certificate2Provider
    {

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
        public KeyVaultX509Certificate2Provider(CertificateClient certificateClient, SecretClient secretClient, ILogger logger)
        {
            this.certificateClient = certificateClient ?? throw new ArgumentNullException(nameof(certificateClient));
            this.secretClient = secretClient ?? throw new ArgumentNullException(nameof(secretClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.cache = new MemoryCache(new MemoryCacheOptions() { });
        }

        /// <summary>
        /// Downloads the certificate from the key vault store.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        async Task<X509Certificate2> LoadCertifcateAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException(nameof(id));

            logger.Information("Loading certificate {CertificateId}.", id);

            // download certificate from key vault
            var crt = await certificateClient.GetCertificateAsync(id, cancellationToken);
            var key = await secretClient.GetSecretAsync(crt.Value.SecretId.ToString(), cancellationToken: cancellationToken);

            // load private key
            var pfx = new X509Certificate2(
                Convert.FromBase64String(key.Value.Value),
                (string)null,
                X509KeyStorageFlags.MachineKeySet);

            // log certificate information
            if (logger.IsEnabled(LogEventLevel.Information))
                logger.Information("Loaded certificate {@Certificate}.", new
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
        /// Gets the X509 certificate with the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<X509Certificate2> AcquireCertificateAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException(nameof(id));

            return cache.GetOrCreateAsync(id, _ => LoadCertifcateAsync(id, cancellationToken));
        }

    }

}
