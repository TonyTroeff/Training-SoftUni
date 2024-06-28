namespace PizzaCalories.Interfaces;

public interface IIngredient
{
    const double BaseWeightModifier = 2;
    
    double Weight { get; }
    double CalculateCalories();
}