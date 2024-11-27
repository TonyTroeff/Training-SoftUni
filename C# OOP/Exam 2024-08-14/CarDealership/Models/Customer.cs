using CarDealership.Models.Contracts;
using CarDealership.Utilities.Messages;

namespace CarDealership.Models;

public abstract class Customer : ICustomer
{
    private readonly List<string> _purchases;

    protected Customer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(ExceptionMessages.NameIsRequired);

        this.Name = name;

        this._purchases = new List<string>();
        this.Purchases = this._purchases.AsReadOnly();
    }

    public string Name { get; }
    public IReadOnlyCollection<string> Purchases { get; }

    public void BuyVehicle(string vehicleModel)
        => this._purchases.Add(vehicleModel);

    public override string ToString()
        => $"{this.Name} - Purchases: {this.Purchases.Count}";
}
