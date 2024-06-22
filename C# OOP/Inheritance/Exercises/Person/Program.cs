namespace Person;

using System;

public static class Program
{
    public static void Main()
    {
        string name = Console.ReadLine();
        int age = int.Parse(Console.ReadLine());

        try
        {
            Child child = new Child(name, age);
            Console.WriteLine(child);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}