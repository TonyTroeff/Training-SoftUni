namespace CyberSecurityDS.Models.Contracts;

public interface ICyberAttack
{
    string AttackName { get; }

    int SeverityLevel { get; }

    bool Status { get; }

    void MarkAsMitigated();
}
