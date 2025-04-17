using CyberSecurityDS.Repositories.Contracts;

namespace CyberSecurityDS.Repositories;

public abstract class BaseRepository<T> : IRepository<T>
    where T : class
{
    private readonly Dictionary<string, T> _models;

    public IReadOnlyCollection<T> Models { get; }

    protected BaseRepository()
    {
        this._models = new Dictionary<string, T>();
        this.Models = this._models.Values;
    }

    public void AddNew(T model)
    {
        string uniqueKey = this.ExtractUniqueKey(model);
        this._models[uniqueKey] = model;
    }

    public bool Exists(string name)
    {
        return this._models.ContainsKey(name);
    }

    public T? GetByName(string name)
    {
        // We could use this._models.TryGetValue(...)
        if (!this.Exists(name)) return null;
        return this._models[name];
    }

    protected abstract string ExtractUniqueKey(T model);
}
