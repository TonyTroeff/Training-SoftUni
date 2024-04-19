using System;

namespace BasketballEquipment
{
    public class Program
    {
        public static void Main()
        {
            int taxPerYear = int.Parse(Console.ReadLine());
            double shoes = 0.6 * taxPerYear;
            double kit = 0.8 * shoes;
            double ball = 0.25 * kit;
            double accessoaries = 0.2 * ball;

            double total = taxPerYear + shoes + kit + ball + accessoaries;
            Console.WriteLine(total);
        }
    }
}