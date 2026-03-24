using BlackFriday.Models.Contracts;
using BlackFriday.Repositories;
using BlackFriday.Repositories.Contracts;

namespace BlackFriday.Models
{
    public class Application : IApplication
    {
        public IRepository<IProduct> Products { get; }
        public IRepository<IUser> Users { get; }

        public Application()
        {
            this.Products = new ProductRepository();
            this.Users = new UserRepository();
        }
    }
}
