namespace Vehicles.II.Interfaces;

public interface IVehicle
{
    double Fuel { get; }
    double Consumption { get; }
    double TankCapacity { get; }
    
    string Travel(double distance);
    string Refuel(double liters);
}