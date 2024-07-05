namespace Raiding.Interfaces.Models;

public interface IHero
{
    string Name { get; }
    int Power { get; }

    string CastAbility();
}