namespace TheContentDepartment.Models.Contracts;

public interface IResource
{
    string Name { get; }

    string Creator { get; }

    int Priority { get; }

    bool IsTested { get; }

    bool IsApproved { get; }

    void Test();
        
    void Approve();
}