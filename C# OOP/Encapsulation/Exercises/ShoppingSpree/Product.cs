namespace ShoppingSpree;

using System;

public class Product
{
    public Product(string name, decimal cost)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
        if (cost < 0) throw new ArgumentException("Money cannot be negative");
        
        this.Name = name;
        this.Cost = cost;
    }

    public string Name { get; }
    public decimal Cost { get; }
}