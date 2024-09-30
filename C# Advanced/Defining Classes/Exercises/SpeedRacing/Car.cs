namespace SpeedRacing;

public class Car
{
    public Car(string model, double fuelAmount, double fuelConsumptionPerKilometer)
    {
        this.Model = model;
        this.FuelAmount = fuelAmount;
        this.FuelConsumptionPerKilometer = fuelConsumptionPerKilometer;
    }

    public string Model { get; }
    public double FuelAmount { get; private set; }
    public double FuelConsumptionPerKilometer { get; }
    public double TravelledDistance { get; private set; }

    public bool Drive(double kilometers)
    {
        double necessaryFuel = this.FuelConsumptionPerKilometer * kilometers;
        if (necessaryFuel > this.FuelAmount) return false;

        this.FuelAmount -= necessaryFuel;
        this.TravelledDistance += kilometers;
        return true;
    }
}
