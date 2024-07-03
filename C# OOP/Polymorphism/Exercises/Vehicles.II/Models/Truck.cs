namespace Vehicles.II.Models;

public class Truck : BaseVehicle
{
    public Truck(double fuel, double consumption, double tankCapacity) : base(fuel, consumption, tankCapacity)
    {
    }

    public override double Consumption => base.Consumption + 1.6;

    public override string Refuel(double liters)
    {
        var canRefuel = this.ValidateCanRefuel(liters);
        if (!string.IsNullOrWhiteSpace(canRefuel)) return canRefuel;

        return base.Refuel(liters * 0.95);
    }
}