namespace CarDealership.Models;

public class SaloonCar : Vehicle
{
    private const double PriceIncreasePercentage = 1.1;

    public SaloonCar(string model, double price)
        : base(model, price * PriceIncreasePercentage)
    {
    }
}
