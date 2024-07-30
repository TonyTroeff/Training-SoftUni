namespace TheContentDepartment.Core;

using System.Text;
using TheContentDepartment.Core.Contracts;
using TheContentDepartment.Models;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories;
using TheContentDepartment.Utilities.Messages;

public class Controller : IController
{
    private readonly ResourceRepository _resources = new();
    private readonly MemberRepository _members = new();

    public string JoinTeam(string memberType, string memberName, string path)
    {
        ITeamMember member;
        if (memberType == nameof(TeamLead)) member = new TeamLead(memberName, path);
        else if (memberType == nameof(ContentMember)) member = new ContentMember(memberName, path);
        else return string.Format(OutputMessages.MemberTypeInvalid, memberType);

        if (this._members.Models.Any(m => m.Path == path)) return OutputMessages.PositionOccupied;
        if (this._members.Models.Any(m => m.Name == memberName)) return string.Format(OutputMessages.MemberExists, memberName);
        
        this._members.Add(member);
        return string.Format(OutputMessages.MemberJoinedSuccessfully, memberName);
    }

    public string CreateResource(string resourceType, string resourceName, string path)
    {
        var creator = this._members.Models.FirstOrDefault(m => m.Path == path);
        if (creator is null) return string.Format(OutputMessages.NoContentMemberAvailable, resourceName);
        if (creator.InProgress.Contains(resourceName)) return string.Format(OutputMessages.ResourceExists, resourceName);
        
        IResource resource;
        if (resourceType == nameof(Exam)) resource = new Exam(resourceName, creator.Name);
        else if (resourceType == nameof(Workshop)) resource = new Workshop(resourceName, creator.Name);
        else if (resourceType == nameof(Presentation)) resource = new Presentation(resourceName, creator.Name);
        else return string.Format(OutputMessages.ResourceTypeInvalid, resourceType);
        
        this._resources.Add(resource);
        creator.WorkOnTask(resourceName);

        return string.Format(OutputMessages.ResourceCreatedSuccessfully, creator.Name, resourceType, resourceName);
    }

    public string LogTesting(string memberName)
    {
        var member = this._members.TakeOne(memberName);
        if (member is null) return OutputMessages.WrongMemberName;

        var resource = this._resources.Models.Where(m => !m.IsTested && m.Creator == memberName).MinBy(m => m.Priority);
        if (resource is null) return string.Format(OutputMessages.NoResourcesForMember, memberName);

        var teamLead = this._members.Models.First(x => x is TeamLead);
        
        member.FinishTask(resource.Name);
        teamLead.WorkOnTask(resource.Name);
        resource.Test();

        return string.Format(OutputMessages.ResourceTested, resource.Name);
    }

    public string ApproveResource(string resourceName, bool isApprovedByTeamLead)
    {
        var resource = this._resources.TakeOne(resourceName);
        if (!resource.IsTested) return string.Format(OutputMessages.ResourceNotTested, resourceName);
        
        var teamLead = this._members.Models.First(x => x is TeamLead);

        if (isApprovedByTeamLead)
        {
            resource.Approve();
            teamLead.FinishTask(resourceName);
            return string.Format(OutputMessages.ResourceApproved, teamLead.Name, resourceName);
        }
        else
        {
            resource.Test();
            return string.Format(OutputMessages.ResourceReturned, teamLead.Name, resourceName);
        }
    }

    public string DepartmentReport()
    {
        var sb = new StringBuilder();
        sb.Append("Finished Tasks:");

        foreach (var resource in this._resources.Models.Where(m => m.IsApproved))
        {
            sb.AppendLine();
            sb.Append("--");
            sb.Append(resource);
        }

        sb.AppendLine();
        sb.AppendLine("Team Report:");

        var teamLead = this._members.Models.Single(m => m is TeamLead);
        sb.Append("--");
        sb.Append(teamLead);

        foreach (var member in this._members.Models)
        {
            if (member == teamLead) continue;
            
            sb.AppendLine();
            sb.Append(member);
        }

        return sb.ToString();
    }
}