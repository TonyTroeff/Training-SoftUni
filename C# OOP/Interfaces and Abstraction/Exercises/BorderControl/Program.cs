namespace BorderControl;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        var entities = new List<IIdentifiable>();

        var input = Console.ReadLine();
        while (input != "End")
        {
            var data = input.Split();
            
            if (data.Length == 2)
            {
                var robot = new Robot(data[0], data[1]);
                entities.Add(robot);
            }
            else if (data.Length == 3)
            {
                var citizen = new Citizen(data[0], int.Parse(data[1]), data[2]);
                entities.Add(citizen);
            }

            input = Console.ReadLine();
        }

        var suffix = Console.ReadLine();

        var detained = entities.Where(e => e.Id.EndsWith(suffix)).ToArray();
        foreach (var entity in detained)
            Console.WriteLine(entity.Id);
    }
}