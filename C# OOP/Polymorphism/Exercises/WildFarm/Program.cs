namespace WildFarm;

using System;
using System.Collections.Generic;
using WildFarm.Animals.Birds;
using WildFarm.Animals.Mammals;
using WildFarm.Animals.Mammals.Feline;
using WildFarm.Foods;
using WildFarm.Interfaces;

public static class Program
{
    public static void Main()
    {
        var animals = new List<IAnimal>();
        var animalInput = Console.ReadLine();

        while (animalInput != "End")
        {
            var animal = CreateAnimal(animalInput);
            
            var foodInput = Console.ReadLine();
            var food = CreateFood(foodInput);

            Console.WriteLine(animal.AskForFood());

            if (!animal.Eat(food)) Console.WriteLine($"{animal.GetType().Name} does not eat {food.GetType().Name}!");
            animals.Add(animal);
            
            animalInput = Console.ReadLine();
        }

        foreach (var animal in animals) Console.WriteLine(animal);
    }

    private static IAnimal CreateAnimal(string input)
    {
        var data = input.Split();

        return data[0] switch
        {
            nameof(Hen) => new Hen(data[1], double.Parse(data[2]), double.Parse(data[3])),
            nameof(Owl) => new Owl(data[1], double.Parse(data[2]), double.Parse(data[3])),
            nameof(Cat) => new Cat(data[1], double.Parse(data[2]), data[3], data[4]),
            nameof(Tiger) => new Tiger(data[1], double.Parse(data[2]), data[3], data[4]),
            nameof(Dog) => new Dog(data[1], double.Parse(data[2]), data[3]),
            nameof(Mouse) => new Mouse(data[1], double.Parse(data[2]), data[3]),
            _ => throw new InvalidOperationException("Invalid animal type")
        };
    }

    private static IFood CreateFood(string input)
    {
        var data = input.Split();

        return data[0] switch
        {
            nameof(Fruit) => new Fruit(int.Parse(data[1])),
            nameof(Meat) => new Meat(int.Parse(data[1])),
            nameof(Seeds) => new Seeds(int.Parse(data[1])),
            nameof(Vegetable) => new Vegetable(int.Parse(data[1])),
            _ => throw new InvalidOperationException("Invalid food type")
        };
    }
}