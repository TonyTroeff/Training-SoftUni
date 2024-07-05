namespace Vehicles.Interfaces;

public interface IVehicle
{
    double Fuel { get; }
    double Consumption { get; }
    
    string Travel(double distance);
    void Refuel(double liters);
}