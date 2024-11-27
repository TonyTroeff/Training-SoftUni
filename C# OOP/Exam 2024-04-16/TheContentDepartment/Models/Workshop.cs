namespace TheContentDepartment.Models;

public class Workshop : Resource
{
    private const int DefaultPriority = 2;

    public Workshop(string name, string creator) : base(name, creator, DefaultPriority)
    {
    }
}