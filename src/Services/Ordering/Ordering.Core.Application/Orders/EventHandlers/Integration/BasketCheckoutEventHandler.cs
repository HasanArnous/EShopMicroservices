using MassTransit;
using BuildingBlocks.Messaging.Events;
using Ordering.Core.Application.Orders.Commands.CreateOrder;

namespace Ordering.Core.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender _sender, ILogger<BasketCheckoutEventHandler> _logger)
	: IConsumer<BasketCheckoutEvent>
{
	public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
	{
		_logger.LogInformation("Integration Event was handled: {IntegrationEvent}", context.Message.GetType().Name);
		var command = MapToCreateOrderCommand(context.Message);
		await _sender.Send(command);
	}

	private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent basket)
	{
		var orderId = Guid.NewGuid();
		return new CreateOrderCommand(
			new OrderDTO(
				orderId,
				basket.CustomerId,
				basket.UserName,
				new AddressDTO(basket.FirstName, basket.LastName, basket.EmailAddress, basket.AddressLine, basket.Country, basket.State, basket.ZipCode),
				new AddressDTO(basket.FirstName, basket.LastName, basket.EmailAddress, basket.AddressLine, basket.Country, basket.State, basket.ZipCode),
				new PaymentDTO(basket.CardName, basket.CardNumber, basket.Expiration, basket.Cvv, basket.PaymentMethod),
				OrderStatus.Pending,
				// HARD CODING HERE, THE CART ITEMS MUST BE EXTRACTED FROM THE 'BasketCheckoutEvent'
				// ONLY TO MAKE THE COURSE EASIER AND FOCUS ON THE BIG PICTURE
				new List<OrderItemDTO>
				{
					new OrderItemDTO(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 1, 500),
					new OrderItemDTO(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 2, 400),
				}
				));
	}
}
