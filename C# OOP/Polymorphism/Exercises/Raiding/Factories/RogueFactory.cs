namespace Raiding.Factories;

using Raiding.Interfaces.Factories;
using Raiding.Interfaces.Models;
using Raiding.Models;

public class RogueFactory : IHeroFactory
{
    public IHero Create(string name) => new Rogue(name);
}