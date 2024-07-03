namespace Vehicles.Models;

public class Truck : BaseVehicle
{
    public Truck(double fuel, double consumption) : base(fuel, consumption)
    {
    }

    public override double Consumption => base.Consumption + 1.6;

    public override void Refuel(double liters) => base.Refuel(liters * 0.95);
}