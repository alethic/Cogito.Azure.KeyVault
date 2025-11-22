using Autofac;

using Cogito.Autofac;
using Cogito.Extensions.Options.Configuration.Autofac;

namespace Cogito.Azure.KeyVault.Autofac
{

    public class AssemblyModule : ModuleBase
    {

        protected override void Register(ContainerBuilder builder)
        {
            builder.RegisterModule<Cogito.Azure.Identity.Autofac.AssemblyModule>();
            builder.RegisterFromAttributes(typeof(AssemblyModule).Assembly);
            builder.Configure<KeyVaultOptions>("Azure:KeyVault");
            builder.RegisterType<DefaultKeyVaultTokenProvider>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CertificateClientFactory>().AsSelf().SingleInstance();
            builder.RegisterType<KeyClientFactory>().AsSelf().SingleInstance();
            builder.RegisterType<SecretClientFactory>().AsSelf().SingleInstance();
            builder.RegisterType<KeyVaultX509Certificate2Provider>().AsSelf();
            builder.Register(ctx => ctx.Resolve<CertificateClientFactory>().CreateSecretClient()).SingleInstance();
            builder.Register(ctx => ctx.Resolve<KeyClientFactory>().CreateKeyClient()).SingleInstance();
            builder.Register(ctx => ctx.Resolve<SecretClientFactory>().CreateSecretClient()).SingleInstance();
        }

    }

}
