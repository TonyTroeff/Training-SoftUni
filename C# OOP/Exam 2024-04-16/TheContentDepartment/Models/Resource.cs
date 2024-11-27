namespace TheContentDepartment.Models;

using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;

public abstract class Resource : IResource
{
    protected Resource(string name, string creator, int priority)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(ExceptionMessages.NameNullOrWhiteSpace);
        this.Name = name;
        this.Creator = creator;
        this.Priority = priority;
    }
    
    public string Name { get; }
    public string Creator { get; }
    public int Priority { get; }
    public bool IsTested { get; private set; }
    public bool IsApproved { get; private set; }

    public void Test() => this.IsTested = !this.IsTested;

    public void Approve() => this.IsApproved = true;

    public override string ToString() => $"{this.Name} ({this.GetType().Name}), Created By: {this.Creator}";
}