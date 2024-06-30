namespace MilitaryElite.Models;

using System.Text;
using MilitaryElite.Interfaces;

public class Spy : Soldier, ISpy
{
    public Spy(int id, string firstName, string lastName, int codeNumber) : base(id, firstName, lastName)
    {
        this.CodeNumber = codeNumber;
    }

    public int CodeNumber { get; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString());
        sb.Append($"Code Number: {this.CodeNumber}");

        return sb.ToString();
    }
}