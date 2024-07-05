namespace Raiding.Interfaces.Factories;

using Raiding.Interfaces.Models;

public interface IHeroFactory
{
    IHero Create(string name);
}