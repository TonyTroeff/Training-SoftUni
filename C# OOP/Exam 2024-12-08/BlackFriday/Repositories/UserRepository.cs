using BlackFriday.Models.Contracts;
using BlackFriday.Repositories.Contracts;
using System.Collections.ObjectModel;

namespace BlackFriday.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private readonly List<IUser> models;

        public IReadOnlyCollection<IUser> Models { get; }

        public UserRepository()
        {
            this.models = new List<IUser>();
            this.Models = new ReadOnlyCollection<IUser>(this.models);
        }

        public void AddNew(IUser model) => this.models.Add(model);

        public bool Exists(string name) => this.models.Any(p => p.UserName == name);

        public IUser GetByName(string name)
            => this.models.FirstOrDefault(x => x.UserName == name);
    }
}
