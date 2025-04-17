namespace CyberSecurityDS.Utilities.Messages;

public static class OutputMessages
{
    public const string TypeInvalid = "{0} is not a valid type for the system.";

    public const string EntryAlreadyExists = "{0} already exists in the system.";

    public const string EntryAddedSuccessfully = "{0}: {1} is added to the system.";

    public const string EntryNotFound = "{0} does not exist in the system.";

    public const string AttackAlreadyAssigned = "{0} is already assigned to {1}.";

    public const string AttackAssignedSuccessfully = "{0} is assigned to {1}.";

    public const string AttackAlreadyMitigated = "{0} is already mitigated.";

    public const string AttackNotAssignedYet = "{0} is not assigned yet.";

    public const string CannotMitigateDueToCompatibility = "{0} cannot mitigate {1}.";

    public const string AttackMitigatedSuccessfully = "{0} is mitigated successfully.";

    public const string SoftwareNotEffectiveEnough = "{0} could not be mitigated by {1}.";
}
