namespace BlackFriday.Models.Contracts
{
    public interface IUser
    {
        string UserName { get; }

        bool HasDataAccess { get; }

        string Email { get; }
    }
}
