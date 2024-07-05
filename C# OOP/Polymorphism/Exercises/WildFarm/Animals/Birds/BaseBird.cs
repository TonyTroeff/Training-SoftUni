namespace WildFarm.Animals.Birds;

public abstract class BaseBird : BaseAnimal
{
    protected BaseBird(string name, double weight, double wingSize) : base(name, weight)
    {
        this.WingSize = wingSize;
    }
    
    public double WingSize { get; }

    public override string ToString() => $"{this.GetType().Name} [{this.Name}, {this.WingSize}, {this.Weight}, {this.FoodEaten}]";
}