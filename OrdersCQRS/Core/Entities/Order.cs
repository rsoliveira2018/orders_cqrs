namespace Core.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> OrderItems { get; set; }
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
