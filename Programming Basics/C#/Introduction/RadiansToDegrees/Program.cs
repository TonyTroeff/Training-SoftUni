using System;

namespace RadiansToDegrees
{
    public class Program
    {
        public static void Main()
        {
            double radians = double.Parse(Console.ReadLine());
            double degrees = radians * 180 / Math.PI;
            Console.WriteLine(degrees);
        }
    }
}