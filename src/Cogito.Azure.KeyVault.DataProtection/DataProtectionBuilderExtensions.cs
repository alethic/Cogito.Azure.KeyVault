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
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static IDataProtectionBuilder ProtectKeysWithAzureKeyVault(this IDataProtectionBuilder builder, string keyName = "dpapi")
        {
            builder.Services.AddTransient<IKeyVaultKeyIdentifierFactory, DefaultKeyVaultKeyIdentifierFactory>();
            return builder.ProtectKeysWithAzureKeyVault(p => p.GetRequiredService<IKeyVaultKeyIdentifierFactory>().GetIdentifier(keyName), p => p.GetRequiredService<IKeyVaultTokenCredentialProvider>().GetCredential());
        }

    }
}
