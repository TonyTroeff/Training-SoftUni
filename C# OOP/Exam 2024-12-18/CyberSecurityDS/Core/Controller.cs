using CyberSecurityDS.Core.Contracts;
using CyberSecurityDS.Models;
using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Utilities.Messages;
using System.Text;

namespace CyberSecurityDS.Core;

public class Controller : IController
{
    private readonly ISystemManager _systemManager = new SystemManager();

    public string AddCyberAttack(string attackType, string attackName, int severityLevel, string extraParam)
    {
        ICyberAttack cyberAttack;
        if (attackType == nameof(PhishingAttack))
            cyberAttack = new PhishingAttack(attackName, severityLevel, extraParam);
        else if (attackType == nameof(MalwareAttack))
            cyberAttack = new MalwareAttack(attackName, severityLevel, extraParam);
        else
            return string.Format(OutputMessages.TypeInvalid, attackType);

        if (this._systemManager.CyberAttacks.Exists(attackName))
            return string.Format(OutputMessages.EntryAlreadyExists, attackName);

        this._systemManager.CyberAttacks.AddNew(cyberAttack);
        return string.Format(OutputMessages.EntryAddedSuccessfully, attackType, attackName);
    }

    public string AddDefensiveSoftware(string softwareType, string softwareName, int effectiveness)
    {
        IDefensiveSoftware defensiveSoftware;
        if (softwareType == nameof(Firewall))
            defensiveSoftware = new Firewall(softwareName, effectiveness);
        else if (softwareType == nameof(Antivirus))
            defensiveSoftware = new Antivirus(softwareName, effectiveness);
        else
            return string.Format(OutputMessages.TypeInvalid, softwareType);

        if (this._systemManager.DefensiveSoftwares.Exists(softwareName))
            return string.Format(OutputMessages.EntryAlreadyExists, softwareName);

        this._systemManager.DefensiveSoftwares.AddNew(defensiveSoftware);
        return string.Format(OutputMessages.EntryAddedSuccessfully, softwareType, softwareName);
    }

    public string AssignDefense(string cyberAttackName, string defensiveSoftwareName)
    {
        ICyberAttack? cyberAttack = this._systemManager.CyberAttacks.GetByName(cyberAttackName);
        if (cyberAttack is null)
            return string.Format(OutputMessages.EntryNotFound, cyberAttackName);

        IDefensiveSoftware? defensiveSoftware = this._systemManager.DefensiveSoftwares.GetByName(defensiveSoftwareName);
        if (defensiveSoftware is null)
            return string.Format(OutputMessages.EntryNotFound, defensiveSoftwareName);

        IDefensiveSoftware? conflictingSoftware = this.ExtractAssignedSoftware(cyberAttack.AttackName);
        if (conflictingSoftware is not null)
            return string.Format(OutputMessages.AttackAlreadyAssigned, cyberAttack.AttackName, conflictingSoftware.Name);

        defensiveSoftware.AssignAttack(cyberAttack.AttackName);
        return string.Format(OutputMessages.AttackAssignedSuccessfully, cyberAttack.AttackName, defensiveSoftware.Name);
    }

    public string GenerateReport()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Security:");
        foreach (IDefensiveSoftware defensiveSoftware in this._systemManager.DefensiveSoftwares.Models
            .OrderBy(ds => ds.Name))
        {
            sb.AppendLine();
            sb.Append(defensiveSoftware.ToString());
        }

        sb.AppendLine();
        
        sb.AppendLine("Threads:");
        sb.Append("-Mitigated");

        foreach (ICyberAttack mitigatedAttack in this._systemManager.CyberAttacks.Models
            .Where(ca => ca.Status)
            .OrderBy(ca => ca.AttackName))
        {
            sb.AppendLine();
            sb.Append(mitigatedAttack.ToString());
        }

        sb.AppendLine();
        sb.Append("-Pending");

        foreach (ICyberAttack pendingAttack in this._systemManager.CyberAttacks.Models
            .Where(ca => !ca.Status)
            .OrderBy(ca => ca.AttackName))
        {
            sb.AppendLine();
            sb.Append(pendingAttack.ToString());
        }

        return sb.ToString();
    }

    public string MitigateAttack(string cyberAttackName)
    {
        ICyberAttack? cyberAttack = this._systemManager.CyberAttacks.GetByName(cyberAttackName);
        if (cyberAttack is null)
            return string.Format(OutputMessages.EntryNotFound, cyberAttackName);

        if (cyberAttack.Status)
            return string.Format(OutputMessages.AttackAlreadyMitigated, cyberAttack.AttackName);

        IDefensiveSoftware? assignedSoftware = this.ExtractAssignedSoftware(cyberAttack.AttackName);
        if (assignedSoftware is null)
            return string.Format(OutputMessages.AttackNotAssignedYet, cyberAttack.AttackName);

        bool areCompatible = (assignedSoftware is Firewall && cyberAttack is MalwareAttack)
            || (assignedSoftware is Antivirus && cyberAttack is PhishingAttack);
        if (!areCompatible)
            return string.Format(OutputMessages.CannotMitigateDueToCompatibility, assignedSoftware.GetType().Name, cyberAttack.GetType().Name);

        if (assignedSoftware.Effectiveness >= cyberAttack.SeverityLevel)
        {
            cyberAttack.MarkAsMitigated();
            return string.Format(OutputMessages.AttackMitigatedSuccessfully, cyberAttack.AttackName);
        }
        else
        {
            return string.Format(OutputMessages.SoftwareNotEffectiveEnough, cyberAttack.AttackName, assignedSoftware.Name);
        }
    }

    private IDefensiveSoftware? ExtractAssignedSoftware(string cyberAttackName)
        => this._systemManager.DefensiveSoftwares.Models
    .FirstOrDefault(ds => ds.AssignedAttacks.Contains(cyberAttackName));
}
