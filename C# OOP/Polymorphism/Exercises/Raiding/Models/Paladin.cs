namespace Raiding.Models;

public class Paladin : BaseHealingHero
{
    private const int DefaultPower = 100;
    
    public Paladin(string name) : base(name, DefaultPower)
    {
    }
}