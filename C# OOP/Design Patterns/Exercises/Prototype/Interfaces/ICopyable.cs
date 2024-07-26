namespace Prototype.Interfaces;

public interface ICopyable<out T>
    where T : ICopyable<T>
{
    T ShallowCopy();
    T DeepCopy();
}