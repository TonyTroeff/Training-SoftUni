namespace Vehicles.II.Models;

using Vehicles.II.Interfaces;

public abstract class BaseVehicle : IVehicle
{
    protected BaseVehicle(double fuel, double consumption, double tankCapacity)
    {
        if (fuel <= tankCapacity) this.Fuel = fuel;
        this.Consumption = consumption;
        this.TankCapacity = tankCapacity;
    }

    public double Fuel { get; private set; }
    public virtual double Consumption { get; }
    public double TankCapacity { get; }

    public string Travel(double distance)
    {
        var requiredFuel = distance * this.Consumption;
        if (this.Fuel < requiredFuel) return $"{this.GetType().Name} needs refueling";

        this.Fuel -= requiredFuel;
        return $"{this.GetType().Name} travelled {distance} km";
    }

    public virtual string Refuel(double liters)
    {
        if (liters <= 0) return "Fuel must be a positive number";

        var canRefuel = this.ValidateCanRefuel(liters);
        if (!string.IsNullOrWhiteSpace(canRefuel)) return canRefuel;

        this.Fuel += liters;
        return string.Empty;
    }
    
    public override string ToString() => $"{this.GetType().Name}: {this.Fuel:f2}";

    protected string ValidateCanRefuel(double liters)
    {
        if (this.Fuel + liters > this.TankCapacity) return $"Cannot fit {liters} fuel in the tank";
        return string.Empty;
    }
}