namespace MilitaryElite;

using System;
using System.Collections.Generic;
using MilitaryElite.Enums;
using MilitaryElite.Interfaces;
using MilitaryElite.Models;

public static class Program
{
    public static void Main()
    {
        var soldiers = new Dictionary<int, ISoldier>();
        var soldiersInOrder = new List<ISoldier>();
        
        var input = Console.ReadLine();
        while (input != "End")
        {
            var data = input.Split();

            ISoldier newSoldier = data[0] switch
            {
                "Private" => ConstructPrivateSoldier(data),
                "LieutenantGeneral" => ConstructLieutenantGeneral(data, soldiers),
                "Engineer" => ConstructEngineer(data),
                "Commando" => ConstructCommando(data),
                "Spy" => ConstructSpy(data),
                _ => null
            };

            if (newSoldier is not null)
            {
                soldiers.Add(newSoldier.Id, newSoldier);
                soldiersInOrder.Add(newSoldier);
            }

            input = Console.ReadLine();
        }

        foreach (var soldier in soldiersInOrder) Console.WriteLine(soldier);
    }

    private static ISoldier ConstructPrivateSoldier(string[] data) => new PrivateSoldier(int.Parse(data[1]), data[2], data[3], decimal.Parse(data[4]));

    private static ISoldier ConstructLieutenantGeneral(string[] data, IReadOnlyDictionary<int, ISoldier> soldiers)
    {
        var lieutenantGeneral = new LieutenantGeneral(int.Parse(data[1]), data[2], data[3], decimal.Parse(data[4]));
        for (var i = 5; i < data.Length; i++)
        {
            var privateId = int.Parse(data[i]);
            if (soldiers.TryGetValue(privateId, out var privateSolder)) lieutenantGeneral.AddPrivate(privateSolder);
        }

        return lieutenantGeneral;
    }

    private static ISoldier ConstructEngineer(string[] data)
    {
        if (!Enum.TryParse<SoldierCorps>(data[5], out var corps)) return null;
        var engineer = new Engineer(int.Parse(data[1]), data[2], data[3], decimal.Parse(data[4]), corps);

        for (var i = 6; i < data.Length; i += 2)
        {
            var repair = new Repair(data[i], int.Parse(data[i + 1]));
            engineer.AddRepair(repair);
        }

        return engineer;
    }

    private static ISoldier ConstructCommando(string[] data)
    {
        if (!Enum.TryParse<SoldierCorps>(data[5], out var corps)) return null;
        var commando = new Commando(int.Parse(data[1]), data[2], data[3], decimal.Parse(data[4]), corps);

        for (var i = 6; i < data.Length; i += 2)
        {
            if (!Enum.TryParse<MissionState>(data[i + 1], out var state)) continue;

            var mission = new Mission(data[i], state);
            commando.AddMission(mission);
        }

        return commando;
    }
    
    private static ISoldier ConstructSpy(string[] data) => new Spy(int.Parse(data[1]), data[2], data[3], int.Parse(data[4]));
}