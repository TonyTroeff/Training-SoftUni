using SimpleSnake.Enums;
using SimpleSnake.GameObjects;
using SimpleSnake.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SimpleSnake.Core;

public class ConsoleOperator : IWriter, IReader
{
    public bool TryReadDirection([NotNullWhen(true)] out Direction? direction)
    {
        direction = null;

        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            // direction = keyInfo.Key switch
            // {
            //     ConsoleKey.UpArrow => Direction.Up,
            //     ConsoleKey.RightArrow => Direction.Right,
            //     ConsoleKey.DownArrow => Direction.Down,
            //     ConsoleKey.LeftArrow => Direction.Left,
            //     _ => null
            // };

            if (keyInfo.Key == ConsoleKey.UpArrow) direction = Direction.Up;
            else if (keyInfo.Key == ConsoleKey.RightArrow) direction = Direction.Right;
            else if (keyInfo.Key == ConsoleKey.DownArrow) direction = Direction.Down;
            else if (keyInfo.Key == ConsoleKey.LeftArrow) direction = Direction.Left;
        }

        return direction is not null;
    }

    public void Write(Point point, char symbol)
    {
        SetCursorPosition(point);
        Console.Write(symbol);
    }

    public void Write(Point point, string text)
    {
        SetCursorPosition(point);
        Console.Write(text);
    }

    private static void SetCursorPosition(Point point)
    {
        Console.CursorLeft = point.X;
        Console.CursorTop = point.Y;
    }

    public bool ReadConfirmation(char expectedKeyChar)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        return keyInfo.KeyChar == expectedKeyChar;
    }
}
