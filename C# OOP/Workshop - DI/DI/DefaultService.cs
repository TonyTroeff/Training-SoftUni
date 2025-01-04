using DI.Demo.Interfaces;

namespace DI.Demo;

public class DefaultService<T> : IService<T>
{
    private readonly IWriter _writer;

    public DefaultService(IWriter writer)
    {
        this._writer = writer ?? throw new ArgumentNullException();
    }

    public T[] GetAll()
    {
        this._writer.Write($"Service, Entity: {typeof(T).Name}, Get All was invoked!");
        return Array.Empty<T>();
    }
}
