using Sample.Domain.AggregatesModel.PersonAggregate;
using MediatR;
using System;
using System.Threading.Tasks;
using Sample.Domain.Events;
using Sample.Infrastructure.Idempotency;

namespace Sample.Api.Application.Commands
{
    public class CreatePersonCommandIdentifiedHandler : IdentifierCommandHandler<CreatePersonCommand, bool>
    {
        
        public CreatePersonCommandIdentifiedHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating order.
        }
    }

    public class CreatePersonCommandHandler : IAsyncRequestHandler<CreatePersonCommand, bool>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMediator _mediator;

        public CreatePersonCommandHandler(IMediator mediator, IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(CreatePersonCommand message)
        {
            _personRepository.Add(new Person(message.FirstName, message.LastName, message.Email));
            
            return await _personRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}