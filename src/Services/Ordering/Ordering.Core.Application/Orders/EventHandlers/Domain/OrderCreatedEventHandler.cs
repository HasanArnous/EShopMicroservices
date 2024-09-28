using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Core.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(IPublishEndpoint publisher, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
	public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
	{
		logger.LogInformation("Domain Event Handled: {DomainEvent}", domainEvent.GetType().Name);
		// Publishing an Integration Event to notify other services that there is a new Order was created, 
		// Order Fullfilment processes like Payment, Shipping, Notification.... will handle this event and act upone it.

		//? NOTE AS A PROBLEM (NEED TO CHECK FROM THE COURSE CREATOR)
		// Shouldn't we have to create a class/record (typed event) for this integration event
		// as the same of what we did in the BasketCheckoutEvent so when we add a consumer class
		// we can specify the type of the event and get the details of the object?????
		// Currently, we are publishing the OrderDTO object instead of a specific type for the integration event.....
		if(await featureManager.IsEnabledAsync("OrderFullfilment")) // Check if the feature is on before execution...
		{
			var orderCreatedIntegrationEvent = domainEvent.order.ToDto();
			await publisher.Publish(orderCreatedIntegrationEvent, cancellationToken);
		}
	}
}
