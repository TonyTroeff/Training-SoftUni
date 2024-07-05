namespace WildFarm.Animals.Birds;

public class Hen : BaseBird
{
    public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize)
    {
    }

    protected override double GrowthCoefficient => 0.35;

    public override string AskForFood() => "Cluck";
}