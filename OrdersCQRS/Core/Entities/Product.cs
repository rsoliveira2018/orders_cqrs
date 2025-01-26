﻿namespace Core.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
