namespace WildFarm.Animals.Mammals;

public abstract class BaseMammal : BaseAnimal
{
    protected BaseMammal(string name, double weight, string livingRegion) : base(name, weight)
    {
        this.LivingRegion = livingRegion;
    }
    
    public string LivingRegion { get; }

    public override string ToString() => $"{this.GetType().Name} [{this.Name}, {this.Weight}, {this.LivingRegion}, {this.FoodEaten}]";
}