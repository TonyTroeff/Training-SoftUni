namespace DI.Demo.Interfaces;

public interface IService<T>
{
    T[] GetAll();
}
