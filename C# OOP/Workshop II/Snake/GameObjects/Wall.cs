namespace Snake.GameObjects;

using global::Snake.Utilities;

public class Wall
{
    public Wall(int width, int height)
    {
        this.Width = width;
        this.Height = height;

        this.TopLeftCorner = new Point(0, 0);
        this.BottomRightCorner = new Point(this.Width - 1, this.Height - 1);
    }

    public Point TopLeftCorner { get; }
    public Point BottomRightCorner { get; }
    public int Width { get; }
    public int Height { get; }
}