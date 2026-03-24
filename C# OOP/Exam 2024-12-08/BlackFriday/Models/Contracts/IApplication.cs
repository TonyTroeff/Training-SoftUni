using BlackFriday.Repositories.Contracts;

namespace BlackFriday.Models.Contracts
{
    public interface IApplication
    {
        IRepository<IProduct> Products { get; }

        IRepository<IUser> Users { get; }
    }
}
