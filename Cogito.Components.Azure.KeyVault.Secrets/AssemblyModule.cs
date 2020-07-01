using Autofac;

using Cogito.Autofac;

namespace Cogito.Components.Azure.KeyVault.Secrets
{

    public class AssemblyModule : ModuleBase
    {

        protected override void Register(ContainerBuilder builder)
        {
            builder.RegisterModule<Cogito.Components.Azure.KeyVault.AssemblyModule>();
            builder.RegisterFromAttributes(typeof(AssemblyModule).Assembly);
            builder.Register(ctx => ctx.Resolve<SecretClientFactory>().CreateSecretClient()).SingleInstance();
        }

    }

}
