namespace FoodShortage;

using System;
using System.Collections.Generic;

public static class Program
{
    public static void Main()
    {
        var entities = new Dictionary<string, IBuyer>();

        var n = int.Parse(Console.ReadLine());

        for (var i = 0; i < n; i++)
        {
            var data = Console.ReadLine().Split();
            if (data.Length == 4)
            {
                var citizen = new Citizen(data[0], int.Parse(data[1]), data[2], data[3]);
                entities[citizen.Name] = citizen;
            }
            else if (data.Length == 3)
            {
                var rebel = new Rebel(data[0], int.Parse(data[1]), data[2]);
                entities[rebel.Name] = rebel;
            }
        }

        var buyerName = Console.ReadLine();
        while (buyerName != "End")
        {
            if (entities.TryGetValue(buyerName, out var buyer)) buyer.BuyFood();

            buyerName = Console.ReadLine();
        }

        var result = 0;
        foreach (var entity in entities.Values) result += entity.Food;

        Console.WriteLine(result);
    }
}