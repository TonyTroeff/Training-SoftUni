namespace TheContentDepartment.Core.Contracts;

public interface IController
{
    string JoinTeam(string memberType, string memberName, string path);

    string CreateResource(string resourceType, string resourceName, string path);

    string LogTesting(string memberName);

    string ApproveResource(string resourceName, bool isApprovedByTeamLead);

    string DepartmentReport();
}