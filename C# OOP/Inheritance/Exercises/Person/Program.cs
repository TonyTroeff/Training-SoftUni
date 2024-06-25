namespace Person;

using System;

public static class Program
{
    public static void Main()
    {
        string name = Console.ReadLine();
        int age = int.Parse(Console.ReadLine());

        if(age < 0)
        {
            Console.WriteLine("Age must be positive!");
            return;
        }

        Person person;
        if (age > 15) person = new Person(name, age);
        else person = new Child(name, age);

        Console.WriteLine(person);
    }
}