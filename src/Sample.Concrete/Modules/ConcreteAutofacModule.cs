using Autofac;
using Sample.Concrete.Repository;
using Sample.Concrete.Repository.Entities;

namespace Sample.Concrete.Modules
{
    public class ConcreteAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseRepository<PersonEntity>>().AsImplementedInterfaces().InstancePerLifetimeScope();
        } 
    }
}