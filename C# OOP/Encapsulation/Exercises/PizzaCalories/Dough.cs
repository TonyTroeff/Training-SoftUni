namespace PizzaCalories;

using System;
using System.Collections.Generic;
using PizzaCalories.Enums;
using PizzaCalories.Interfaces;

public class Dough : IIngredient
{
    private const int MinWeight = 1;
    private const int MaxWeight = 200;
    
    private static readonly Dictionary<FlourType, double> FlourTypeModifiers = new() { [FlourType.White] = 1.5, [FlourType.Wholegrain] = 1 };
    private static readonly Dictionary<BakingTechnique, double> BakingTechniqueModifiers = new() { [BakingTechnique.Crispy] = 0.9, [BakingTechnique.Chewy] = 1.1, [BakingTechnique.Homemade] = 1 };
    
    public Dough(string flourType, string bakingTechnique, double weight)
    {
        if (!Enum.TryParse<FlourType>(flourType, ignoreCase: true, out var parsedFlourType) || !Enum.TryParse<BakingTechnique>(bakingTechnique, ignoreCase: true, out var parsedBakingTechnique)) throw new ArgumentException("Invalid type of dough.");
        if (weight < MinWeight || weight > MaxWeight) throw new ArgumentException($"Dough weight should be in the range [{MinWeight}..{MaxWeight}].");

        this.Weight = weight;
        this.FlourType = parsedFlourType;
        this.BakingTechnique = parsedBakingTechnique;
    }
    
    public double Weight { get; }
    public FlourType FlourType { get; }
    public BakingTechnique BakingTechnique { get; }

    public double CalculateCalories() => IIngredient.BaseWeightModifier * this.Weight * FlourTypeModifiers[this.FlourType] * BakingTechniqueModifiers[this.BakingTechnique];
}