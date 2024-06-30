namespace MilitaryElite.Models;

using MilitaryElite.Interfaces;

public class PrivateSoldier : Soldier, IPrivateSoldier
{
    public PrivateSoldier(int id, string firstName, string lastName, decimal salary) : base(id, firstName, lastName)
    {
        this.Salary = salary;
    }

    public decimal Salary { get; }

    public override string ToString() => $"{base.ToString()} Salary: {this.Salary:f2}";
}