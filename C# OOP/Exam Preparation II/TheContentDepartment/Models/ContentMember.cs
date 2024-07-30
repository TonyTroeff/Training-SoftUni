namespace TheContentDepartment.Models;

using TheContentDepartment.Utilities.Messages;

public class ContentMember : TeamMember
{
    private static readonly HashSet<string> AllowedPaths = new() { "CSharp", "JavaScript", "Python", "Java" };

    public ContentMember(string name, string path) : base(name, path)
    {
        if (!AllowedPaths.Contains(path)) throw new ArgumentException(string.Format(ExceptionMessages.PathIncorrect, path));
    }

    public override string ToString() => $"{this.Name} - {this.Path} path. {base.ToString()}";
}