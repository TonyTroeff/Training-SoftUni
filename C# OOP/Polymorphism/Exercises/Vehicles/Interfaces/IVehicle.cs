namespace Vehicles.Interfaces;

public interface IVehicle
{
    double Fuel { get; }
    double Consumption { get; }
    
    bool Travel(double distance);
    void Refuel(double liters);
}