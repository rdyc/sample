using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample.Domain.Events;

namespace Sample.Api.Application.DomainEventHandlers
{
    public class PersonAddedDomainEventHandler : IAsyncNotificationHandler<PersonAddedDomainEvent>
    { 
        private readonly ILoggerFactory _logger;
        //private readonly IBuyerRepository _buyerRepository;
        //private readonly IIdentityService _identityService;
        
        public PersonAddedDomainEventHandler(ILoggerFactory logger/*, IBuyerRepository buyerRepository, IIdentityService identityService*/)
        {
            //_buyerRepository = buyerRepository ?? throw new ArgumentNullException(nameof(buyerRepository));
            //_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PersonAddedDomainEvent notification)
        {
            _logger.CreateLogger(nameof(PersonAddedDomainEventHandler))
                .LogInformation($"Person added name: {notification.FirstName} {notification.LastName} email: {notification.Email}.");
            
            await Task.FromResult("OK");
        }
    }
}
