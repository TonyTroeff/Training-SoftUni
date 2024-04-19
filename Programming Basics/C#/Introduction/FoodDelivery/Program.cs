using System;

namespace FoodDelivery
{
    public class Program
    {
        public static void Main()
        {
            int chicken = int.Parse(Console.ReadLine());
            int fish = int.Parse(Console.ReadLine());
            int vegetarian = int.Parse(Console.ReadLine());

            double basePrice = chicken * 10.35 + fish * 12.40 + vegetarian * 8.15;
            double desertPrice = 0.2 * basePrice;

            double total = basePrice + desertPrice + 2.5;
            Console.WriteLine(total);
        }
    }
}