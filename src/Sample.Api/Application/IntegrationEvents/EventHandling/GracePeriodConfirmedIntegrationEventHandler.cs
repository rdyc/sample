using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Sample.Domain.AggregatesModel.PersonAggregate;
using Ordering.API.Application.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class GracePeriodConfirmedIntegrationEventHandler : IIntegrationEventHandler<GracePeriodConfirmedIntegrationEvent>
    {
        private readonly IPersonRepository _personRepository;

        public GracePeriodConfirmedIntegrationEventHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// Event handler which confirms that the grace period
        /// has been completed and order will not initially be cancelled.
        /// Therefore, the order process continues for validation. 
        /// </summary>
        /// <param name="event">       
        /// </param>
        /// <returns></returns>
        public async Task Handle(GracePeriodConfirmedIntegrationEvent @event)
        {
            var orderToUpdate = await _personRepository.GetAsync(@event.OrderId);
            //orderToUpdate.SetAwaitingValidationStatus();
            await _personRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
