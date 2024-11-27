using CarDealership.Repositories.Contracts;

namespace CarDealership.Repositories;

public abstract class BaseRepository<T> : IRepository<T>
    where T : class
{
    protected Dictionary<string, T> ModelsByUniqueId { get; } = new Dictionary<string, T>();

    public IReadOnlyCollection<T> Models => this.ModelsByUniqueId.Values;

    public abstract void Add(T model);

    public bool Exists(string text) => this.ModelsByUniqueId.ContainsKey(text);

    public T Get(string text)
    {
        if (!this.Exists(text)) return null;
        return this.ModelsByUniqueId[text];
    }

    public bool Remove(string text) => this.ModelsByUniqueId.Remove(text);
}
