namespace Ordering.Core.Domain.ValueObjects;

public record Payment
{
    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public DateTime ExpirationDate { get; } = default!;
    public string CCV { get; } = default!;
    public int PaymentMethod { get; } = default!;

}
