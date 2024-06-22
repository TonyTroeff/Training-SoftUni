namespace ShoppingSpree;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Person
{
    private readonly List<Product> _products;

    public Person(string name, decimal money)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
        if (money < 0) throw new ArgumentException("Money cannot be negative");

        this.Name = name;
        this.Money = money;
        
        this._products = new List<Product>();
        this.Products = this._products.AsReadOnly();
    }
    
    public string Name { get; }
    public decimal Money { get; private set; }
    public IReadOnlyCollection<Product> Products { get; }

    public bool TryPurchase(Product product)
    {
        if (product.Cost > this.Money) return false;
        
        this._products.Add(product);
        this.Money -= product.Cost;
        return true;
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(this.Name);
        stringBuilder.Append(" - ");

        if (this.Products.Count == 0) stringBuilder.Append("Nothing bought");
        else stringBuilder.Append(string.Join(", ", this.Products.Select(p => p.Name)));

        return stringBuilder.ToString();
    }
}