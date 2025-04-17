using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Utilities.Messages;

namespace CyberSecurityDS.Models;

public abstract class DefensiveSoftware : IDefensiveSoftware
{
    private readonly List<string> _assignedAttacks;

    public string Name { get; }
    public int Effectiveness { get; }
    public IReadOnlyCollection<string> AssignedAttacks { get; }

    protected DefensiveSoftware(string name, int effectiveness)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(ExceptionMessages.SoftwareNameRequired);
        if (effectiveness < 0)
            throw new ArgumentException(ExceptionMessages.EffectivenessNegative);

        this.Name = name;
        this.Effectiveness = Math.Max(1, Math.Min(10, effectiveness));

        this._assignedAttacks = new List<string>();
        this.AssignedAttacks = this._assignedAttacks.AsReadOnly();
    }

    public void AssignAttack(string attackName)
    {
        this._assignedAttacks.Add(attackName);
    }

    public override string ToString()
    {
        string attacksInfo;
        if (this.AssignedAttacks.Count > 0) attacksInfo = string.Join(", ", this.AssignedAttacks);
        else attacksInfo = "[None]";

        return $"Defensive Software: {this.Name}, Effectiveness: {this.Effectiveness}, Assigned Attacks: {attacksInfo}";
    }
}
