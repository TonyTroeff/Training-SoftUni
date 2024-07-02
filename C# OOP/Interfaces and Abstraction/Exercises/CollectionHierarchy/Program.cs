namespace CollectionHierarchy;

using System;
using CollectionHierarchy.Interfaces;

public static class Program
{
    public static void Main()
    {
        AddCollection addCollection = new();
        AddRemoveCollection addRemoveCollection = new();
        MyList myList = new();

        string[] values = Console.ReadLine().Split();
        
        AddAll(values, addCollection);
        AddAll(values, addRemoveCollection);
        AddAll(values, myList);

        int n = int.Parse(Console.ReadLine());
        RemoveExactly(n, addRemoveCollection);
        RemoveExactly(n, myList);
    }

    private static void AddAll(string[] values, ISupportsAdd collection)
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (i > 0) Console.Write(' ');
            Console.Write(collection.Add(values[i]));
        }

        Console.WriteLine();
    }

    private static void RemoveExactly(int count, ISupportsRemove collection)
    {
        for (int i = 0; i < count; i++)
        {
            if (i > 0) Console.Write(' ');
            Console.Write(collection.Remove());
        }

        Console.WriteLine();
    }
}