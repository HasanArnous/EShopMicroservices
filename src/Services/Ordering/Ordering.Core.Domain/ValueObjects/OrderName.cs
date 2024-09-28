namespace Ordering.Core.Domain.ValueObjects;

public record OrderName
{
    private const int _defaultLength = 7;
    public string Value { get; }


    private OrderName(string value) => Value = value;

    public static OrderName Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        // Will be disabled to make the course more easier and focus on the big picture of the microservices..
        //ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, _defaultLength);
        return new OrderName(value);
    }
}