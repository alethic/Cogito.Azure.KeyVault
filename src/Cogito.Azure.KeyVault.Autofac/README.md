# Cogito.Azure.KeyVault.Autofac

Registers the Key Vault client factories in an Autofac container.

## Install

```shell
dotnet add package Cogito.Azure.KeyVault.Autofac
```

## Use

```csharp
builder.RegisterAllAssemblyModules();
```

`SecretClientFactory`, `KeyClientFactory` and `CertificateClientFactory` then resolve, configured
from `KeyVaultOptions` and using the container's `TokenCredential`.

## License

MIT.
