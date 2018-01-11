using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Module = Autofac.Module;

namespace Sample.Api.Events
{
    
    public interface IEvent
    {
    }

    public interface IEntity
    {
        IEnumerable<IEvent> Events { get; }
    }
    
    public abstract class Entity : IEntity
    {
        private readonly IDictionary<Type, IEvent> _events = new Dictionary<Type, IEvent>();

        public IEnumerable<IEvent> Events => _events.Values;

        protected void AddEvent(IEvent @event)
        {
            _events[@event.GetType()] = @event;
        }

        protected void ClearEvents()
        {
            _events.Clear();
        }
    }
    
    public class UserEmailChanged : IEvent
    {
        public Guid Id { get; }
 
        public UserEmailChanged(Guid id)
        {
            Id = id;
        }
    }
    
    public class User : Entity
    {
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
 
        protected User()
        {
        }
 
        public User(string email)
        {
            SetEmail(email);
        }
 
        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email can not be empty.", nameof(email));
 
            /*if (Email.Equals(email))
                return;*/
 
            Email = email;
            UpdatedAt = DateTime.UtcNow;
            
            AddEvent(new UserEmailChanged(new Guid()));
        }
    }

    
    
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
 
    public class UserEmailChangedHandler : IEventHandler<UserEmailChanged>
    {
        public async Task HandleAsync(UserEmailChanged @event)
        {
            //Fetch the user from database by id, log this event, store some data etc.
            var _event = @event;
            
            Debug.WriteLine(_event);
        }
    }
    
    public interface IEventDispatcher
    {
        Task DispatchAsync<T>(params T[] events) where T : IEvent;
        //Task DispatchAsync<T>(T command) where T : IEvent;
    }
 
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _context;
 
        public EventDispatcher(IComponentContext context)
        {
            _context = context;
        }
 
        //I need to find out how to get generic handler in Autofac
        public async Task DispatchAsync<T>(params T[] events) where T : IEvent
        {
            foreach (var @event in events)
            {
                if (@event == null)
                    throw new ArgumentNullException(nameof(@event), "Event can not be null.");
 
                var eventType = @event.GetType();
                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                object handler;
                _context.TryResolve(handlerType, out handler);
 
                if (handler == null)
                    return;
 
                //GetRuntimeMethods() works with .NET Core, otherwise simply use GetMethod()
                var method = handler.GetType()
                    .GetMethods()
                    .First(x => x.Name.Equals("HandleAsync"));
 
                await (Task) ((dynamic) handler).HandleAsync(@event);
                
                //var handler2 = _context.Resolve<IEventHandler<T>>();
                //await handler.HandleAsync(@event);
            }
        }
        
        /*public async Task DispatchAsync<T>(T command) where T : IEvent
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command),
                    $"Command: '{typeof(T).FirstName}' can not be null.");
            }
            var handler = _context.Resolve<IEventHandler<T>>();
            await handler.HandleAsync(command);
        }*/
    }
    
    public class EventModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventDispatcher>()
                .As<IEventDispatcher>()
                .InstancePerLifetimeScope();

            //var assembly = typeof(EventModule).GetTypeInfo().Assembly;
            var assembly = Assembly.Load(new AssemblyName("Sample.Api"));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEventHandler<>)).InstancePerLifetimeScope();
        }
    }

//Remember to register the module within the IContainer
    public static class Container
    {
        public static IContainer Resolve()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<EventModule>();

            return builder.Build();
        }
    }
    
    public interface IUserService
    {
        Task CreateAsync(string email);
    }

    public class UserService : IUserService
    {
        private readonly IEventDispatcher _eventDispatcher;

        public UserService(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public async Task CreateAsync(string email)
        {
            var user = new User(email);
            //Store the user somehwere safe using the repository etc.
              
            //Dispatch all of the user events
            await _eventDispatcher.DispatchAsync(user.Events.ToArray());
        }
    }
}