using System;

namespace Cogito.Azure.KeyVault.DataProtection
{

    /// <summary>
    /// Provides an interface to generate key vault keys.
    /// </summary>
    public interface IKeyVaultKeyIdentifierFactory
    {

        /// <summary>
        /// Gets the identifier for the key name.
        /// </summary>
        /// <returns></returns>
        Uri GetIdentifier(string keyName);

    }

}