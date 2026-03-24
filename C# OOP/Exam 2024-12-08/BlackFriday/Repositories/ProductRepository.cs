using BlackFriday.Models.Contracts;
using BlackFriday.Repositories.Contracts;
using System.Collections.ObjectModel;

namespace BlackFriday.Repositories
{
    public class ProductRepository : IRepository<IProduct>
    {
        private readonly List<IProduct> models;

        public IReadOnlyCollection<IProduct> Models { get; }

        public ProductRepository()
        {
            this.models = new List<IProduct>();
            this.Models = new ReadOnlyCollection<IProduct>(this.models);
        }

        public void AddNew(IProduct model) => this.models.Add(model);

        public bool Exists(string name) => this.models.Any(p => p.ProductName == name);

        public IProduct GetByName(string name)
            => this.models.FirstOrDefault(x => x.ProductName == name);
    }
}
