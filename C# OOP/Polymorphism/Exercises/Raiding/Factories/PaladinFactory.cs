namespace Raiding.Factories;

using Raiding.Interfaces.Factories;
using Raiding.Interfaces.Models;
using Raiding.Models;

public class PaladinFactory : IHeroFactory
{
    public IHero Create(string name) => new Paladin(name);
}