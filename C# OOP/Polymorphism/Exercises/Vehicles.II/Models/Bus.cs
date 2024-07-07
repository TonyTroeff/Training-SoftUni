namespace Vehicles.II.Models;

public class Bus : BaseVehicle
{
    public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
        : base(fuelQuantity, fuelConsumption, tankCapacity)
    {
    }

    protected override double ConsumptionIncrease => 1.4;
}