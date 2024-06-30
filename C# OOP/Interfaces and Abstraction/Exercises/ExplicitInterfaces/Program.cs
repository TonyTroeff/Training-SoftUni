namespace ExplicitInterfaces;

using System;

public static class Program
{
    public static void Main()
    {
        var input = Console.ReadLine();
        while (input != "End")
        {
            var data = input.Split();
            var citizen = new Citizen(data[0], data[1], int.Parse(data[2]));

            Console.WriteLine(((IPerson)citizen).Name);
            Console.WriteLine(((IResident)citizen).Name);

            input = Console.ReadLine();
        }
    }
}