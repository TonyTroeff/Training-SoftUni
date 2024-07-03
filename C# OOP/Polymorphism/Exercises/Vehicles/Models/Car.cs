namespace Vehicles.Models;

public class Car : BaseVehicle
{
    public Car(double fuel, double consumption) : base(fuel, consumption)
    {
    }

    public override double Consumption => base.Consumption + 0.9;
}