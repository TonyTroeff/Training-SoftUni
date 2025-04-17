using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Utilities.Messages;

namespace CyberSecurityDS.Models;

public abstract class CyberAttack : ICyberAttack
{
    public string AttackName { get; }
    public int SeverityLevel { get; }
    public bool Status { get; private set; }

    protected CyberAttack(string attackName, int severityLevel)
    {
        if (string.IsNullOrWhiteSpace(attackName))
            throw new ArgumentException(ExceptionMessages.CyberAttackNameRequired);
        if (severityLevel < 0)
            throw new ArgumentException(ExceptionMessages.SeverityLevelNegative);

        this.AttackName = attackName;
        this.SeverityLevel = Math.Max(1, Math.Min(10, severityLevel));
    }

    public void MarkAsMitigated()
    {
        this.Status = true;
    }

    public override string ToString()
        => $"Attack: {this.AttackName}, Severity: {this.SeverityLevel}";
}
