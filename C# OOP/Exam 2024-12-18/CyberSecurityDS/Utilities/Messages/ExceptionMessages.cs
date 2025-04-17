namespace CyberSecurityDS.Utilities.Messages;

public static class ExceptionMessages
{
    //ICyberAttack
    public const string CyberAttackNameRequired = "Attack name is required.";
    public const string SeverityLevelNegative = "Severity level cannot assign negative values.";
    public const string TargetMailRequired = "Target mail is required.";

    //IDefensiveSoftware
    public const string SoftwareNameRequired = "Software name is required.";
    public const string InfectedFilesCountRequired = "Infected files count is required.";
    public const string EffectivenessNegative = "Effectiveness cannot assign negative values.";

}
