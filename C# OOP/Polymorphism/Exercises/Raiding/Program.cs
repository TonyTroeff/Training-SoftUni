namespace Raiding;

using System;
using System.Collections.Generic;
using Raiding.Factories;
using Raiding.Interfaces.Factories;
using Raiding.Interfaces.Models;

public static class Program
{
    public static void Main()
    {
        var factories = new Dictionary<string, IHeroFactory> { ["Druid"] = new DruidFactory(), ["Paladin"] = new PaladinFactory(), ["Rogue"] = new RogueFactory(), ["Warrior"] = new WarriorFactory() };
        var heroes = new List<IHero>();

        var n = int.Parse(Console.ReadLine());
        while (heroes.Count < n)
        {
            var heroName = Console.ReadLine();
            var heroType = Console.ReadLine();
            
            if (factories.TryGetValue(heroType, out var factory)) heroes.Add(factory.Create(heroName));
            else Console.WriteLine("Invalid hero!");
        }

        var bossPower = int.Parse(Console.ReadLine());
        var raidPower = 0;

        foreach (var hero in heroes)
        {
            Console.WriteLine(hero.CastAbility());
            raidPower += hero.Power;
        }
        
        if (raidPower >= bossPower) Console.WriteLine("Victory!");
        else Console.WriteLine("Defeat...");
    }
}