using CyberSecurityDS.Utilities.Messages;

namespace CyberSecurityDS.Models;

public class PhishingAttack : CyberAttack
{
    public string TargetMail { get; }

    public PhishingAttack(string attackName, int severityLevel, string targetMail)
        : base(attackName, severityLevel)
    {
        if (string.IsNullOrWhiteSpace(targetMail))
            throw new ArgumentException(ExceptionMessages.TargetMailRequired);

        this.TargetMail = targetMail;
    }

    public override string ToString()
        => $"{base.ToString()} (Target Mail: {this.TargetMail})";
}
