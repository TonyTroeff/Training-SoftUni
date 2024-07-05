namespace Raiding.Models;

public abstract class BaseHealingHero : BaseHero
{
    protected BaseHealingHero(string name, int power) : base(name, power)
    {
    }

    public sealed override string CastAbility() => $"{this.GetType().Name} - {this.Name} healed for {this.Power}";
}