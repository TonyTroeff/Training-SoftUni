namespace Vehicles.II.Models;

using System;
using Vehicles.II.Interfaces;

public abstract class BaseVehicle : IVehicle
{
    protected BaseVehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
    {
        if (fuelQuantity <= tankCapacity) this.FuelQuantity = fuelQuantity;

        this.FuelConsumption = fuelConsumption;
        this.TankCapacity = tankCapacity;
    }

    public double FuelQuantity { get; private set; }
    public double FuelConsumption { get; }
    public double TankCapacity { get; }

    protected virtual double ConsumptionIncrease { get; }

    public bool Drive(double distance, bool ecoMode)
    {
        if (distance < 0) throw new ArgumentException("Distance must not be negative");

        var fuelConsumption = this.FuelConsumption;
        if (!ecoMode) fuelConsumption += this.ConsumptionIncrease;

        var requiredFuel = distance * fuelConsumption;
        if (requiredFuel > this.FuelQuantity) return false;

        this.FuelQuantity -= requiredFuel;
        return true;
    }

    public virtual bool Refuel(double liters)
    {
        if (liters <= 0) throw new ArgumentException("Fuel must be a positive number");

        var newFuelQuantity = this.FuelQuantity + liters;
        if (newFuelQuantity > this.TankCapacity) return false;

        this.FuelQuantity += liters;
        return true;
    }

    public override string ToString() => $"{this.GetType().Name}: {this.FuelQuantity:f2}";
}