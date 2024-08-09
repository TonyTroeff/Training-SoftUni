namespace Snake.GameObjects;

using global::Snake.Utilities;

public class Food
{
    public Food(Point position, int points, char symbol)
    {
        this.Position = position;
        this.Points = points;
        this.Symbol = symbol;
    }

    public Point Position { get; }
    public int Points { get; }
    public char Symbol { get; }
}