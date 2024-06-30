namespace MilitaryElite.Models;

using System.Collections.Generic;
using System.Text;
using MilitaryElite.Enums;
using MilitaryElite.Interfaces;

public class Commando : SpecializedSoldier, ICommando
{
    private readonly List<IMission> _missions;
    
    public Commando(int id, string firstName, string lastName, decimal salary, SoldierCorps corps) : base(id, firstName, lastName, salary, corps)
    {
        this._missions = new List<IMission>();
        this.Missions = this._missions.AsReadOnly();
    }

    public IReadOnlyCollection<IMission> Missions { get; }

    public bool AddMission(IMission mission)
    {
        if (mission is null) return false;
        
        this._missions.Add(mission);
        return true;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString());
        sb.AppendLine("Missions:");

        for (var i = 0; i < this._missions.Count; i++)
        {
            if (i > 0) sb.AppendLine();

            sb.Append("  ");
            sb.Append(this._missions[i]);
        }
        
        return sb.ToString();;
    }
}