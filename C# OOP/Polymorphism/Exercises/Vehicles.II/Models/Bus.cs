namespace Vehicles.II.Models;

public class Bus : BaseVehicle
{
    public Bus(double fuel, double consumption, double tankCapacity)
        : base(fuel, consumption, tankCapacity)
    {
    }

    public override double Consumption => base.Consumption + (this.IsEmpty ? 0 : 1.4);

    public bool IsEmpty { get; set; } = false;
}