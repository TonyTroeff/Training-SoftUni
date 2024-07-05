namespace WildFarm.Foods;

using WildFarm.Interfaces;

public abstract class BaseFood : IFood
{
    protected BaseFood(int quantity)
    {
        this.Quantity = quantity;
    }

    public int Quantity { get; }
}