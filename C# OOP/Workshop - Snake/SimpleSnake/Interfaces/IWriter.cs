using SimpleSnake.GameObjects;

namespace SimpleSnake.Interfaces;

public interface IWriter
{
    void Write(Point point, char symbol);
    void Write(Point point, string text);
}
