namespace Composite.Interfaces;

public interface IGift
{
    string Description { get; }

    int CalculateTotalPrice();
}