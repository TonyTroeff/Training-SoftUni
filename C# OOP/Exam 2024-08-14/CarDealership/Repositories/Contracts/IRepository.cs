namespace CarDealership.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        IReadOnlyCollection<T> Models { get; }

        void Add(T model);

        bool Remove(string text);

        bool Exists(string text);

        T Get(string text);
    }
}
