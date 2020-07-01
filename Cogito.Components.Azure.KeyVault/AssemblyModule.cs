using Autofac;

using Cogito.Autofac;

namespace Cogito.Components.Azure.KeyVault
{

    public class AssemblyModule : ModuleBase
    {

        protected override void Register(ContainerBuilder builder)
        {
            builder.RegisterModule<Cogito.Components.Azure.Identity.AssemblyModule>();
            builder.RegisterFromAttributes(typeof(AssemblyModule).Assembly);
            builder.Register(ctx => ctx.Resolve<CertificateClientFactory>().CreateSecretClient()).SingleInstance();
            builder.Register(ctx => ctx.Resolve<KeyClientFactory>().CreateKeyClient()).SingleInstance();
            builder.Register(ctx => ctx.Resolve<SecretClientFactory>().CreateSecretClient()).SingleInstance();
        }

    }

}
