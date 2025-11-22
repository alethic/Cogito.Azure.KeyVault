namespace Cogito.Azure.KeyVault.DataProtection
{

    /// <summary>
    /// Provides an interface to generate key vault keys.
    /// </summary>
    public interface IKeyVaultKeyIdentifierFactory
    {

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns></returns>
        string GetIdentifier(string keyName);

    }

}