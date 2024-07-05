namespace WildFarm.Animals.Mammals.Feline;

public abstract class BaseFeline : BaseMammal
{
    protected BaseFeline(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion)
    {
        this.Breed = breed;
    }
    
    public string Breed { get; }

    public override string ToString() => $"{this.GetType().Name} [{this.Name}, {this.Breed}, {this.Weight}, {this.LivingRegion}, {this.FoodEaten}]";
}