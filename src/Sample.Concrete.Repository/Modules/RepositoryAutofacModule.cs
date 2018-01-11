using Autofac;
using Microsoft.EntityFrameworkCore;

namespace Sample.Concrete.Repository.Modules
{
    public class RepositoryAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DbContextOptions>().InstancePerLifetimeScope();
            builder.RegisterType<SampleContext>().InstancePerLifetimeScope();
        }
    }
}