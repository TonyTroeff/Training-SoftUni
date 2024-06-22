namespace AnimalFarm.Models;

using System;

public class Chicken
{
    private const int MinAge = 0;
    private const int MaxAge = 15;

    internal Chicken(string name, int age)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.");
        if (age < MinAge || age > MaxAge) throw new ArgumentException($"Age should be between {MinAge} and {MaxAge}.");
        
        this.Name = name;
        this.Age = age;
    }

    public string Name { get; }
    public int Age { get; }

    public double ProductPerDay
    {
        get { return this.CalculateProductPerDay(); }
    }

    private double CalculateProductPerDay()
    {
        return this.Age switch
        {
            <= 3 => 1.5,
            <= 7 => 2,
            <= 11 => 1,
            _ => 0.75
        };
    }
}