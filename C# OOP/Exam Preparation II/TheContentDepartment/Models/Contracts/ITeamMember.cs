namespace TheContentDepartment.Models.Contracts;

public interface ITeamMember
{
    string Name { get; }

    string Path { get; }

    IReadOnlyCollection<string> InProgress { get; }

    void WorkOnTask(string resourceName);

    void FinishTask(string resourceName);
}