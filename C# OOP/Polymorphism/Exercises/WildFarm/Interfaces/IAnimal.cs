namespace WildFarm.Interfaces;

public interface IAnimal
{
    string Name { get; }
    double Weight { get; }
    int FoodEaten { get; }

    bool Eat(IFood food);
    string AskForFood();
}