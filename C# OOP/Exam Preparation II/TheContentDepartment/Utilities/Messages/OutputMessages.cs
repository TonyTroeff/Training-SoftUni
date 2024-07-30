
namespace TheContentDepartment.Utilities.Messages;

public class OutputMessages
{
    //Common
    public const string PathParamInvalid = "{0} is not a valid parameter.";
    public const string WrongMemberName = "Provide the correct member name.";

    //Join Team
    public const string MemberTypeInvalid = "{0} is not a valid member type.";
    public const string PositionOccupied = "Position is occupied.";
    public const string MemberExists = "{0} has already joined the team.";
    public const string MemberJoinedSuccessfully = "{0} has joined the team. Welcome!";

    //CreateResource        
    public const string ResourceTypeInvalid = "{0} type is not handled by Content Department.";
    public const string NoContentMemberAvailable = "No content member is able to create the {0} resource.";
    public const string ResourceExists = "The {0} resource is being created.";
    public const string ResourceCreatedSuccessfully = "{0} created {1} - {2}.";

    //LogTesting        
    public const string NoResourcesForMember = "{0} has no resources for testing.";
    public const string ResourceTested = "{0} is tested and ready for approval.";

    //ApproveResource
    public const string WrongCreator = "Error! {0} does not match to {1} creator.";
    public const string ResourceNotTested = "{0} cannot be approved without being tested.";
    public const string ResourceApproved = "{0} approved {1}.";
    public const string ResourceReturned = "{0} returned {1} for a re-test.";
}