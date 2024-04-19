using System;

namespace UsdToBgn
{
    public class Program
    {
        public static void Main()
        {
            double usd = double.Parse(Console.ReadLine());
            double bgn = usd * 1.79549;

            Console.WriteLine(bgn);
        }
    }
}