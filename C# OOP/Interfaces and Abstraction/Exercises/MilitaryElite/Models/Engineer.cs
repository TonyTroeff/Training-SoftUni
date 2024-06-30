namespace MilitaryElite.Models;

using System.Collections.Generic;
using System.Text;
using MilitaryElite.Enums;
using MilitaryElite.Interfaces;

public class Engineer : SpecializedSoldier, IEngineer
{
    private readonly List<IRepair> _repairs;
    
    public Engineer(int id, string firstName, string lastName, decimal salary, SoldierCorps corps) : base(id, firstName, lastName, salary, corps)
    {
        this._repairs = new List<IRepair>();
        this.Repairs = this._repairs.AsReadOnly();
    }

    public IReadOnlyCollection<IRepair> Repairs { get; }

    public bool AddRepair(IRepair repair)
    {
        if (repair is null) return false;
        
        this._repairs.Add(repair);
        return true;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString());
        sb.AppendLine("Repairs:");

        for (var i = 0; i < this._repairs.Count; i++)
        {
            if (i > 0) sb.AppendLine();

            sb.Append("  ");
            sb.Append(this._repairs[i]);
        }
        
        return sb.ToString();
    }
}