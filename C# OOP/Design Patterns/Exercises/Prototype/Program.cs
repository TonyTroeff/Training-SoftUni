namespace Prototype;

public static class Program
{
    public static void Main()
    {
        var veggies = new[] { "Lettuce", "Tomato" };
        var sandwich = new Sandwich("Wheat", "Bacon", "Cheddar", veggies);

        var shallowCopy = sandwich.ShallowCopy();
        var deepCopy = sandwich.DeepCopy();

        veggies[0] = "Cucumber";

        Console.WriteLine($"Original: {sandwich}");
        Console.WriteLine($"Shallow copy: {shallowCopy}");
        Console.WriteLine($"Deep copy: {deepCopy}");
    }
}