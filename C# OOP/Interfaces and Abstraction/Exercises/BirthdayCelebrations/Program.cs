namespace BirthdayCelebrations;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        var identifiableEntities = new List<IIdentifiable>();
        var birthableEntities = new List<IBirthable>();
        
        var input = Console.ReadLine();
        while (input != "End")
        {
            var data = input.Split();

            if (data[0] == "Citizen")
            {
                var citizen = new Citizen(data[1], int.Parse(data[2]), data[3], data[4]);
                identifiableEntities.Add(citizen);
                birthableEntities.Add(citizen);
            }
            else if (data[0] == "Pet")
            {
                var pet = new Pet(data[1], data[2]);
                birthableEntities.Add(pet);
            }
            else if (data[0] == "Robot")
            {
                var robot = new Robot(data[1], data[2]);
                identifiableEntities.Add(robot);
            }

            input = Console.ReadLine();
        }

        var suffix = Console.ReadLine();

        var result = birthableEntities.Where(e => e.Birthdate.EndsWith(suffix)).ToArray();
        foreach (var entity in result)
            Console.WriteLine(entity.Birthdate);
    }
}