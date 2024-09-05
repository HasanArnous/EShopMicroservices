namespace Ordering.Core.Application.DTOs;

public record PaymentDTO(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);
