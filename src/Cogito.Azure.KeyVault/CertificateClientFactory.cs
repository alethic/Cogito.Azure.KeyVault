using System;

using Azure.Security.KeyVault.Certificates;

using Microsoft.Extensions.Options;

namespace Cogito.Azure.KeyVault
{

    /// <summary>
    /// Provides instances of an Azure Key Vault certificate client.
    /// </summary>
    public class CertificateClientFactory
    {

        readonly IOptions<KeyVaultOptions> _options;
        readonly IKeyVaultTokenCredentialProvider _credentialProvider;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="credentialProvider"></param>
        public CertificateClientFactory(IOptions<KeyVaultOptions> options, IKeyVaultTokenCredentialProvider credentialProvider)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _credentialProvider = credentialProvider ?? throw new ArgumentNullException(nameof(credentialProvider));
        }

        /// <summary>
        /// Creates a new key vault certificate client.
        /// </summary>
        /// <returns></returns>
        public CertificateClient CreateCertificateClient()
        {
            if (_options.Value.VaultUri is null)
                throw new InvalidOperationException("VaultUri has not been configured.");

            return new CertificateClient(_options.Value.VaultUri, _credentialProvider.GetCredential(), new CertificateClientOptions()
            {
                DisableChallengeResourceVerification = _options.Value.DisableChallengeResourceVerification ?? false,
            });
        }

    }

}
