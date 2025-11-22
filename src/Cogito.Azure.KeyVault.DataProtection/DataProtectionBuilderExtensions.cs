using System;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Cogito.Azure.KeyVault.DataProtection
{

    /// <summary>
    /// Contains Azure KeyVault-specific extension methods for modifying a <see cref="IDataProtectionBuilder"/> to use the <see cref="IKeyVaultTokenCredentialProvider"/>.
    /// </summary>
    public static class DataProtectionBuilderExtensions
    {

        /// <summary>
        /// Configures the data protection system to protect keys with specified key in Azure KeyVault. This uses the <see cref="IKeyVaultTokenCredentialProvider"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="keyIdentifier"></param>
        /// <returns></returns>
        public static IDataProtectionBuilder ProtectKeysWithAzureKeyVault(this IDataProtectionBuilder builder, string keyIdentifier)
        {
            return builder.ProtectKeysWithAzureKeyVault(keyIdentifier, p => p.GetRequiredService<IKeyVaultTokenCredentialProvider>().GetCredential());
        }

        /// <summary>
        /// Configures the data protection system to protect keys with specified key in Azure KeyVault. This uses the <see cref="IKeyVaultTokenCredentialProvider"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="keyIdentifier"></param>
        /// <returns></returns>
        public static IDataProtectionBuilder ProtectKeysWithAzureKeyVault(this IDataProtectionBuilder builder, Uri keyIdentifier)
        {
            return builder.ProtectKeysWithAzureKeyVault(keyIdentifier, p => p.GetRequiredService<IKeyVaultTokenCredentialProvider>().GetCredential());
        }

        /// <summary>
        /// Configures the data protection system to protect keys with specified key in Azure KeyVault. This uses the <see cref="IKeyVaultTokenCredentialProvider"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="keyIdentifierFactory"></param>
        /// <returns></returns>
        public static IDataProtectionBuilder ProtectKeysWithAzureKeyVault(this IDataProtectionBuilder builder, Func<IServiceProvider, string> keyIdentifierFactory)
        {
            return builder.ProtectKeysWithAzureKeyVault(keyIdentifierFactory, p => p.GetRequiredService<IKeyVaultTokenCredentialProvider>().GetCredential());
        }

        /// <summary>
        /// Configures the data protection system to protect keys with specified key in Azure KeyVault. This uses the <see cref="IKeyVaultTokenCredentialProvider"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="keyIdentifierFactory"></param>
        /// <returns></returns>
        public static IDataProtectionBuilder ProtectKeysWithAzureKeyVault(this IDataProtectionBuilder builder, Func<IServiceProvider, Uri> keyIdentifierFactory)
        {
            return builder.ProtectKeysWithAzureKeyVault(keyIdentifierFactory, p => p.GetRequiredService<IKeyVaultTokenCredentialProvider>().GetCredential());
        }

    }
}
