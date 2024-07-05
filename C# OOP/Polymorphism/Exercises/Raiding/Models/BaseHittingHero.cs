namespace Raiding.Models;

public abstract class BaseHittingHero : BaseHero
{
    protected BaseHittingHero(string name, int power) : base(name, power)
    {
    }

    public sealed override string CastAbility() => $"{this.GetType().Name} - {this.Name} hit for {this.Power} damage";
}