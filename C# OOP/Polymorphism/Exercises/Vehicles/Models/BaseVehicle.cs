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

    public string Travel(double distance)
    {
        var requiredFuel = distance * this.Consumption;
        if (this.Fuel < requiredFuel) return $"{this.GetType().Name} needs refueling";

        this.Fuel -= requiredFuel;
        return $"{this.GetType().Name} travelled {distance} km";
    }

    public virtual void Refuel(double liters) => this.Fuel += liters;

    public override string ToString() => $"{this.GetType().Name}: {this.Fuel:f2}";
}