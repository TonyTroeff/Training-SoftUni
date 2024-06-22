using System;
using System.Collections.Generic;
using ShoppingSpree;

public static class Program
{
    public static void Main()
    {
        var peopleInOrder = new List<Person>();
        var peopleMap = new Dictionary<string, Person>();
        var productsMap = new Dictionary<string, Product>();
        
        try
        {
            ReadPeople(peopleInOrder, peopleMap);
            ReadProducts(productsMap);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        
        var command = Console.ReadLine();
        while (command != "END")
        {
            var data = command.Split();

            var person = peopleMap[data[0]];
            var product = productsMap[data[1]];
            
            if (!person.TryPurchase(product)) Console.WriteLine($"{person.Name} can't afford {product.Name}");
            else Console.WriteLine($"{person.Name} bought {product.Name}");
            
            command = Console.ReadLine();
        }

        foreach (var person in peopleInOrder) Console.WriteLine(person);
    }

    private static void ReadPeople(List<Person> peopleInOrder, Dictionary<string, Person> peopleMap)
    {
        var peopleInput = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
     
        for (var i = 0; i < peopleInput.Length; i++)
        {
            var data = peopleInput[i].Split('=');
            var person = new Person(data[0], decimal.Parse(data[1]));
            
            peopleInOrder.Add(person);
            peopleMap[person.Name] = person;
        }
    }

    private static void ReadProducts(Dictionary<string, Product> productsMap)
    {
        var productsInput = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
      
        for (var i = 0; i < productsInput.Length; i++)
        {
            var data = productsInput[i].Split('=');
            var product = new Product(data[0], decimal.Parse(data[1]));
            
            productsMap[product.Name] = product;
        }
    }
}