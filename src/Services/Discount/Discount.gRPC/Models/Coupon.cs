namespace Discount.gRPC.Models;

public class Coupon
{
    public int Id { get; set; }
    public Guid ProductId { get; set; }
    public string Description { get; set; } = default!;
    public double Amount { get; set; }
}
