namespace CarDealership.Models;

public class Truck : Vehicle
{
    private const double PriceIncreasePercentage = 1.3;

    public Truck(string model, double price)
        : base(model, price * PriceIncreasePercentage)
    {
    }
}
