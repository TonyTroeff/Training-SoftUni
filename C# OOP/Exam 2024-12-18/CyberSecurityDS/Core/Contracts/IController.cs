namespace CyberSecurityDS.Core.Contracts;

public interface IController
{
    string AddCyberAttack(string attackType, string attackName, int severityLevel, string extraParam);

    string AddDefensiveSoftware(string softwareType, string softwareName, int effectiveness);

    string AssignDefense(string cyberAttackName, string defensiveSoftwareName);

    string MitigateAttack(string cyberAttackName);

    string GenerateReport();
}
