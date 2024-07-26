namespace Template;

using Template.Interfaces;

public static class Program
{
    public static void Main()
    {
        var foods = new List<IFood> { new TwelveGrain(), new Sourdough(), new WholeWheat() };

        foreach (var food in foods)
            Console.WriteLine(food.PrepareRecipe());
    }
}