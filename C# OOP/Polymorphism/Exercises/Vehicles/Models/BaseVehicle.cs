namespace Vehicles.Models;

using Vehicles.Interfaces;

public abstract class BaseVehicle : IVehicle
{
    protected BaseVehicle(double fuel, double consumption)
    {
        this.Fuel = fuel;
        this.Consumption = consumption;
    }

    public double Fuel { get; private set; }
    public virtual double Consumption { get; }

    public bool Travel(double distance)
    {
        var requiredFuel = distance * this.Consumption;
        if (this.Fuel < requiredFuel) return false;

        this.Fuel -= requiredFuel;
        return true;
    }

    public virtual void Refuel(double liters) => this.Fuel += liters;

    public override string ToString() => $"{this.GetType().Name}: {this.Fuel:f2}";
}