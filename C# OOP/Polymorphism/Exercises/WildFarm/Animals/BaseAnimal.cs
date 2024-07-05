namespace WildFarm.Animals;

using WildFarm.Interfaces;

public abstract class BaseAnimal : IAnimal
{
    protected BaseAnimal(string name, double weight)
    {
        this.Name = name;
        this.Weight = weight;
    }

    public string Name { get; }
    public double Weight { get; private set; }
    public int FoodEaten { get; private set; }

    protected abstract double GrowthCoefficient { get; }
    
    public virtual bool Eat(IFood food)
    {
        this.Weight += food.Quantity * this.GrowthCoefficient;
        this.FoodEaten += food.Quantity;
        return true;
    }

    public abstract string AskForFood();
}