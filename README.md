# Cogito.Azure.KeyVault

[![Build](https://github.com/alethic/Cogito.Azure.KeyVault/actions/workflows/Cogito.Azure.KeyVault.yml/badge.svg)](https://github.com/alethic/Cogito.Azure.KeyVault/actions/workflows/Cogito.Azure.KeyVault.yml)

Configured client factories for Azure Key Vault, including loading X.509 certificates and protecting Data Protection keys.

## Packages

**[Cogito.Azure.KeyVault](https://www.nuget.org/packages/Cogito.Azure.KeyVault)** — Configured client factories for Azure Key Vault, and an X.509 certificate provider that loads from a vault.

**[Cogito.Azure.KeyVault.Autofac](https://www.nuget.org/packages/Cogito.Azure.KeyVault.Autofac)** — Registers the Key Vault client factories in an Autofac container.

**[Cogito.Azure.KeyVault.DataProtection](https://www.nuget.org/packages/Cogito.Azure.KeyVault.DataProtection)** — Protects ASP.NET Core Data Protection keys with a key from Azure Key Vault.

Each package carries its own README with the detail; the links above go to nuget.org.

## Building

```shell
dotnet restore Cogito.Azure.KeyVault.sln
dotnet msbuild -p:Configuration=Release Cogito.Azure.KeyVault.dist.msbuildproj
```

Packages are staged into `dist/nuget` and test suites into `dist/tests`; run a suite with
`dotnet test -f <tfm> <path to its assembly>`.

## License

MIT — see [LICENSE](LICENSE).
