namespace Core.Entities;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public required Customer Customer { get; set; }
    public required DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public required List<OrderItem> OrderItems { get; set; }
}

public enum OrderStatus
{
    WaitingPayment = 0,
    PaymentCompleted = 1,
    UnderWay = 2,
    Delivered = 3,
    RejectedAtDelivery = 4,
    PaymentDenied = 5,
}
