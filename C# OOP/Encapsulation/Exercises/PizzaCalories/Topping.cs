namespace PizzaCalories;

using System;
using System.Collections.Generic;
using PizzaCalories.Enums;
using PizzaCalories.Interfaces;

public class Topping : IIngredient
{
    private const int MinWeight = 1;
    private const int MaxWeight = 50;
    
    private static readonly Dictionary<ToppingType, double> ToppingTypeModifiers = new() { [ToppingType.Meat] = 1.2, [ToppingType.Veggies] = 0.8, [ToppingType.Cheese] = 1.1, [ToppingType.Sauce] = 0.9 };
    
    public Topping(string toppingType, double weight)
    {
        if (!Enum.TryParse<ToppingType>(toppingType, ignoreCase: true, out var parsedToppingType)) throw new ArgumentException($"Cannot place {toppingType} on top of your pizza.");
        if (weight < MinWeight || weight > MaxWeight) throw new ArgumentException($"{toppingType} weight should be in the range [{MinWeight}..{MaxWeight}].");

        this.Weight = weight;
        this.Type = parsedToppingType;
    }
    
    public double Weight { get; }
    public ToppingType Type { get; }
    
    public double CalculateCalories() => IIngredient.BaseWeightModifier * this.Weight * ToppingTypeModifiers[this.Type];
}