using System.Collections.Generic;
using Autofac;
using Autofac.Features.Variance;
using MediatR;
using Sample.Api.Application.Commands;
using Sample.Api.Events;
using Sample.Api.Notifications;
//using Sample.Concrete.Services;
//using Sample.Contract.Services;
using PingHandler = Sample.Api.Events.PingHandler;

namespace Sample.Api.Modules
{
    public class ApiAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method in ConfigureServices.
            /*  builder.Register(c => new ValuesService(c.Resolve<ILogger<ValuesService>>()))
                 .As<IValuesService>()
                 .InstancePerLifetimeScope(); */


            // enables contravariant Resolve() for interfaces with single contravariant ("in") arg
            builder.RegisterSource(new ContravariantRegistrationSource());

            // mediator itself
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // request handlers
            builder
                .Register<SingleInstanceFactory>(ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t =>
                    {
                        object o;
                        return c.TryResolve(t, out o) ? o : null;
                    };
                })
                .InstancePerLifetimeScope();

            // notification handlers
            builder
                .Register<MultiInstanceFactory>(ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t => (IEnumerable<object>) c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
                })
                .InstancePerLifetimeScope();

            // finally register our custom code (individually, or via assembly scanning)
            // - requests & handlers as transient, i.e. InstancePerDependency()
            // - pre/post-processors as scoped/per-request, i.e. InstancePerLifetimeScope()
            // - behaviors as transient, i.e. InstancePerDependency()
            //builder.RegisterAssemblyTypes(typeof(MyType).GetTypeInfo().Assembly).AsImplementedInterfaces(); // via assembly scan
            builder.RegisterType<PingHandler>().AsImplementedInterfaces().InstancePerDependency();          // or individually
            builder.RegisterType<OneWayHandlerWithBaseClass>().AsImplementedInterfaces().InstancePerDependency();          // or individually
            builder.RegisterType<Pong1>().AsImplementedInterfaces().InstancePerDependency();          // or individually
            builder.RegisterType<Pong2>().AsImplementedInterfaces().InstancePerDependency();          // or individually

            //builder.RegisterType<ArtistService>().As<IArtistService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
    }
}