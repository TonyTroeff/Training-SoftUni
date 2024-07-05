namespace Raiding.Models;

public class Rogue : BaseHittingHero
{
    private const int DefaultPower = 80;
    
    public Rogue(string name) : base(name, DefaultPower)
    {
    }
}