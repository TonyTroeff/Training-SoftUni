namespace Composite;

using Composite.Interfaces;

public class Gift : IGift
{
    private readonly int _price;

    public Gift(string description, int price)
    {
        this.Description = description;
        this._price = price;
    }

    public string Description { get; }

    public int CalculateTotalPrice() => this._price;

    public override string ToString() => $"{this.Description} -> {this._price}";
}