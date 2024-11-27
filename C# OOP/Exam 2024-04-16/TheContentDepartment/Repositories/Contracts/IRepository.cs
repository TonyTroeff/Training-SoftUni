namespace TheContentDepartment.Repositories.Contracts;

public interface IRepository<T> where T : class
{
    IReadOnlyCollection<T> Models { get; }

    void Add(T model);

    T TakeOne(string modelName);
}