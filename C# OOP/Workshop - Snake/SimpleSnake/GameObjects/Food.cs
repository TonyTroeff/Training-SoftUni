namespace SimpleSnake.GameObjects;

public class Food
{
    public Food(Point position, int score, char symbol)
    {
        this.Position = position;
        this.Score = score;
        this.Symbol = symbol;
    }

    public Point Position { get; }
    public int Score { get; }
    public char Symbol { get; }
}
