namespace Raiding.Models;

public class Druid : BaseHealingHero
{
    private const int DefaultPower = 80;
    
    public Druid(string name) : base(name, DefaultPower)
    {
    }
}