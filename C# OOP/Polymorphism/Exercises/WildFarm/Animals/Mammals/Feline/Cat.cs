namespace WildFarm.Animals.Mammals.Feline;

using WildFarm.Foods;
using WildFarm.Interfaces;

public class Cat : BaseFeline
{
    public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
    {
    }

    protected override double GrowthCoefficient => 0.3;

    public override bool Eat(IFood food) => food is Vegetable or Meat && base.Eat(food);

    public override string AskForFood() => "Meow";
}