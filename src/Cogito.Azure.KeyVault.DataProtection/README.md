# Cogito.Azure.KeyVault.DataProtection

Protects ASP.NET Core Data Protection keys with a key from Azure Key Vault.

## Why

A load-balanced application must share its Data Protection key ring, and the ring itself has to be
encrypted at rest with a key the application does not store. Key Vault is the natural place for that
key; this wires it up.

## Install

```shell
dotnet add package Cogito.Azure.KeyVault.DataProtection
```

## Use

```csharp
services.AddDataProtection()
    .PersistKeysToAzureBlobStorage(...)
    .ProtectKeysWithAzureKeyVault();
```

The vault key is resolved through `IKeyVaultKeyIdentifierFactory` and the credential the container
already has, so the vault URI and identity are not repeated here.

## License

MIT.
