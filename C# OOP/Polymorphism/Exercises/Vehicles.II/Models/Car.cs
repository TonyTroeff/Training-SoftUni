namespace Vehicles.II.Models;

public class Car : BaseVehicle
{
    public Car(double fuel, double consumption, double tankCapacity) : base(fuel, consumption, tankCapacity)
    {
    }

    public override double Consumption => base.Consumption + 0.9;
}