using Autofac;
using Sample.Api.Application.Queries;
using Sample.Domain.AggregatesModel.PersonAggregate;
using Sample.Infrastructure.Idempotency;
using Sample.Infrastructure.Repositories;

namespace Sample.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new PersonQueries(QueriesConnectionString))
                .As<IPersonQueries>()
                .InstancePerLifetimeScope();

            /*builder.RegisterType<BuyerRepository>()
                .As<IBuyerRepository>()
                .InstancePerLifetimeScope();*/

            builder.RegisterType<PersonRepository>()
                .As<IPersonRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
                .As<IRequestManager>()
                .InstancePerLifetimeScope();
        }
    }
}