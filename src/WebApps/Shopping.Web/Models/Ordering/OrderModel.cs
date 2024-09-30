﻿namespace Shopping.Web.Models.Ordering;

public record OrderModel
(
	Guid Id,
	Guid CustomerId,
    string OrderName,
	AddressModel BillingAddress,
    AddressModel ShippingAddress,
	PaymentModel Payment,
    OrderStatus Status,
	List<OrderItemModel> OrderItems
);

public record AddressModel(string FirstName, string LastName, string? EmailAddress, string? AddressLine, string Country, string State, string ZipCode);
public record PaymentModel(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);
public record OrderItemModel(Guid OrderId, Guid ProductId, int Quantity, decimal Price);

public enum OrderStatus
{
	Draft = 1,
	Pending = 2,
	Completed = 3,
	Canceled = 4
}

// Wrapper Clases
public record GetOrdersResponse(PaginatedResult<OrderModel> PaginatedResult);
public record GetOrdersByCustomerResponse(IEnumerable<OrderModel> Orders);
public record GetOrdersByNameResponse(IEnumerable<OrderModel> Orders);