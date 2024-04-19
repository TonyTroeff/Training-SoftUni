using System;

namespace Repainting
{
    public class Program
    {
        public static void Main()
        {
            int nylon = int.Parse(Console.ReadLine());
            int paint = int.Parse(Console.ReadLine());
            int diluent = int.Parse(Console.ReadLine());
            int hours = int.Parse(Console.ReadLine());

            double materialsCost = 1.5 * (nylon + 2) + 14.5 * (paint * 1.1) + diluent * 5 + 0.4;
            double wage = 0.3 * materialCosts;

            double total = materialCosts + wage * hours;
            Console.WriteLine(total);
        }
    }
}