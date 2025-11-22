using Microsoft.AspNetCore.DataProtection;

namespace Cogito.Azure.KeyVault.DataProtection
{

    public static class DataProtectionBuilderExtensions
    {

        public static IDataProtectionBuilder ProtectKeysWithAzureKeyVault(this IDataProtectionBuilder builder)
        {
            Argument.AssertNotNull(builder, nameof(builder));
            Argument.AssertNotNull(keyResolverFactory, nameof(keyResolverFactory));
            Argument.AssertNotNullOrEmpty(keyIdentifier, nameof(keyIdentifier));
        }

    }
}
