namespace TheContentDepartment.Models;

using TheContentDepartment.Utilities.Messages;

public class TeamLead : TeamMember
{
    private static readonly HashSet<string> AllowedPaths = new() { "Master" };
    
    public TeamLead(string name, string path) : base(name, path)
    {
        if (!AllowedPaths.Contains(path)) throw new ArgumentException(string.Format(ExceptionMessages.PathIncorrect, path));
    }

    public override string ToString() => $"{this.Name} ({this.GetType().Name}) - {base.ToString()}";
}