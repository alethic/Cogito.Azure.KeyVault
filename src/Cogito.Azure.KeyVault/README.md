# Cogito.Azure.KeyVault

Configured client factories for Azure Key Vault, and an X.509 certificate provider that loads from a
vault.

## Why

Each Key Vault client needs a vault URI and a credential. Constructing them at each call site spreads
that configuration through the application and makes the credential hard to change.

## Install

```shell
dotnet add package Cogito.Azure.KeyVault
```

## Use

```csharp
var secrets = secretClientFactory.Create();
var secret = await secrets.GetSecretAsync("connection-string");
```

`SecretClientFactory`, `KeyClientFactory` and `CertificateClientFactory` each build a client from
`KeyVaultOptions` and the ambient credential. `IKeyVaultTokenCredentialProvider` is the seam for
supplying that credential; the default uses the one from `Cogito.Azure.Identity`.

`KeyVaultX509Certificate2Provider` loads a certificate — private key included — straight out of the
vault, for code that wants an `X509Certificate2` rather than a vault reference.

## License

MIT.
