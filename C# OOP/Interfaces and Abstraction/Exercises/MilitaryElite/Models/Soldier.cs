namespace MilitaryElite.Models;

using MilitaryElite.Interfaces;

public abstract class Soldier : ISoldier
{
    protected Soldier(int id, string firstName, string lastName)
    {
        this.Id = id;
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public override string ToString() => $"Name: {this.FirstName} {this.LastName} Id: {this.Id}";
}