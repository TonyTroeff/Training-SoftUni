namespace CyberSecurityDS.Models.Contracts;

public interface IDefensiveSoftware
{
    string Name { get; }

    int Effectiveness { get; }

    IReadOnlyCollection<string> AssignedAttacks { get; }

    void AssignAttack(string attackName);
}
