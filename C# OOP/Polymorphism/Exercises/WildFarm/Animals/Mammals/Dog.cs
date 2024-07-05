namespace WildFarm.Animals.Mammals;

using WildFarm.Foods;
using WildFarm.Interfaces;

public class Dog : BaseMammal
{
    public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
    {
    }

    protected override double GrowthCoefficient => 0.4;

    public override bool Eat(IFood food) => food is Meat && base.Eat(food);

    public override string AskForFood() => "Woof!";
}