namespace TheContentDepartment.Models;

using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;

public abstract class TeamMember : ITeamMember
{
    private readonly List<string> _inProgress;
    
    protected TeamMember(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(ExceptionMessages.NameNullOrWhiteSpace);
        
        this.Name = name;
        this.Path = path;

        this._inProgress = new List<string>();
        this.InProgress = this._inProgress.AsReadOnly();
    }
    
    public string Name { get; }
    public string Path { get; }
    public IReadOnlyCollection<string> InProgress { get; }

    public void WorkOnTask(string resourceName) => this._inProgress.Add(resourceName);

    public void FinishTask(string resourceName) => this._inProgress.Remove(resourceName);

    public override string ToString() => $"Currently working on {this.InProgress.Count} tasks.";
}