namespace MilitaryElite.Models;

using System.Text;
using MilitaryElite.Enums;
using MilitaryElite.Interfaces;

public abstract class SpecializedSoldier : PrivateSoldier, ISpecializedSoldier
{
    protected SpecializedSoldier(int id, string firstName, string lastName, decimal salary, SoldierCorps corps) : base(id, firstName, lastName, salary)
    {
        this.Corps = corps;
    }

    public SoldierCorps Corps { get; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString());
        sb.Append($"Corps: {this.Corps}");
        
        return sb.ToString();
    }
}