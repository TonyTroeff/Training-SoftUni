namespace CarDealership.Models;

public class SUV : Vehicle
{
    private const double PriceIncreasePercentage = 1.2;

    public SUV(string model, double price)
        : base(model, price * PriceIncreasePercentage)
    {
    }
}
