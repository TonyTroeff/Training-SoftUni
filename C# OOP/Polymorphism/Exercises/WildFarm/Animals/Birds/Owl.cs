namespace WildFarm.Animals.Birds;

using WildFarm.Foods;
using WildFarm.Interfaces;

public class Owl : BaseBird
{
    public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
    {
    }

    protected override double GrowthCoefficient => 0.25;

    public override bool Eat(IFood food) => food is Meat && base.Eat(food);

    public override string AskForFood() => "Hoot Hoot";
}