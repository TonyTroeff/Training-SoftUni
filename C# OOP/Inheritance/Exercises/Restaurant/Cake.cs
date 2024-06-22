namespace Restaurant;

public class Cake : Dessert
{
    private const decimal DefaultPrice = 5m;
    private const double DefaultGrams = 250;
    private const double DefaultCalories = 1000;
    
    public Cake(string name) : base(name, DefaultPrice, DefaultGrams, DefaultCalories)
    {
    }
}