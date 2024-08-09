namespace Snake.Utilities;

using System;
using System.Text;

public static class ConsoleHelper
{
    public static void CustomizeConsole()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Clear();
        Console.CursorVisible = false;
    }

    public static void Write(Point point, char symbol)
        => Write(point.X, point.Y, symbol);

    public static void Write(int x, int y, char symbol)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(symbol);
    }

    public static void Write(Point point, string text)
        => Write(point.X, point.Y, text);

    public static void Write(int x, int y, string text)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(text);
    }

    public static void WriteLine(Point point, string text)
        => WriteLine(point.X, point.Y, text);

    public static void WriteLine(int x, int y, string text)
    {
        Console.SetCursorPosition(x, y);
        Console.WriteLine(text);
    }
}