namespace Snake.Core;

using System;
using System.Collections.Generic;
using System.Threading;
using Snake.GameObjects;
using Snake.Utilities;

public class Engine
{
    private const char WallSymbol = '\u25A0';
    private const char SnakeSymbol = '\u25CF';
    private const int FoodCount = 5;

    private static readonly Point DefaultDirection = new(1, 0);

    private static readonly Dictionary<ConsoleKey, Point> Directions = new()
    {
        [ConsoleKey.RightArrow] = DefaultDirection,
        [ConsoleKey.LeftArrow] = new Point(-1, 0),
        [ConsoleKey.UpArrow] = new Point(0, -1),
        [ConsoleKey.DownArrow] = new Point(0, 1)
    };

    private static readonly Func<Point, Food>[] FoodFactories =
    {
        p => new Food(p, 1, '*'),
        p => new Food(p, 2, '$'),
        p => new Food(p, 3, '#'),
    };

    private readonly Wall _wall;

    public Engine(Wall wall)
    {
        ArgumentNullException.ThrowIfNull(wall);
        this._wall = wall;
    }

    public void Run()
    {
        bool restart;
        do
        {
            restart = this.Play();
        } while (restart);

        this.PrintGameOver();
    }

    private bool Play()
    {
        var foodMap = new Dictionary<Point, Food>();

        var snake = new Snake(this._wall.TopLeftCorner + new Point(1, 1), DefaultDirection);

        this.DrawPlayground();
        PrepareSnake(snake, 6);

        for (var i = 0; i < FoodCount; i++) this.PrepareRandomFood(snake, foodMap);

        var speed = 100f;
        var score = 0;

        this.PrintSpeed(speed);
        this.PrintScore(score);

        while (true)
        {
            if (Console.KeyAvailable)
            {
                var pressedKeyInfo = Console.ReadKey();
                TryChangeDirection(snake, pressedKeyInfo.Key);
            }

            snake.GrowFront();
            if (this.MeetsWall(snake.Head) || snake.IntersectsWith(snake.Head, ignoreHead: true))
            {
                var shouldRestart = this.AskForRestart();
                if (shouldRestart) ClearPlayground(snake, foodMap);

                return shouldRestart;
            }

            ConsoleHelper.Write(snake.Head, SnakeSymbol);

            if (foodMap.TryGetValue(snake.Head, out var collectedFood))
            {
                score += collectedFood.Points;
                this.PrintScore(score);

                foodMap.Remove(collectedFood.Position);
                this.PrepareRandomFood(snake, foodMap);
            }
            else
            {
                ConsoleHelper.Write(snake.ShortenBack(), ' ');
            }

            speed = Math.Max(speed - 0.1f, 10);
            this.PrintSpeed(speed);

            Thread.Sleep((int) speed);
        }
    }

    private static void ClearPlayground(Snake snake, Dictionary<Point, Food> foodMap)
    {
        while (snake.Length > 1) ConsoleHelper.Write(snake.ShortenBack(), ' ');

        foreach (var point in foodMap.Keys) ConsoleHelper.Write(point, ' ');
        foodMap.Clear();
    }

    private static void PrepareSnake(Snake snake, int elementsCount)
    {
        ConsoleHelper.Write(snake.Head, SnakeSymbol);
        for (var i = 1; i < elementsCount; i++) ConsoleHelper.Write(snake.GrowFront(), SnakeSymbol);
    }

    private static void TryChangeDirection(Snake snake, ConsoleKey key)
    {
        if (!Directions.TryGetValue(key, out var newDirection)) return;

        var accumulated = snake.Direction + newDirection;
        if (accumulated is { X: 0, Y: 0 }) return;

        snake.Direction = newDirection;
    }

    private bool MeetsWall(Point p) => p.X == this._wall.TopLeftCorner.X || p.X == this._wall.BottomRightCorner.X || p.Y == this._wall.TopLeftCorner.Y || p.Y == this._wall.BottomRightCorner.Y;

    private void DrawPlayground()
    {
        for (var i = this._wall.TopLeftCorner.X; i <= this._wall.BottomRightCorner.X; i++)
        {
            ConsoleHelper.Write(i, this._wall.TopLeftCorner.Y, WallSymbol);
            ConsoleHelper.Write(i, this._wall.BottomRightCorner.Y, WallSymbol);
        }

        for (var j = this._wall.TopLeftCorner.Y + 1; j < this._wall.BottomRightCorner.Y; j++)
        {
            ConsoleHelper.Write(this._wall.TopLeftCorner.X, j, WallSymbol);
            ConsoleHelper.Write(this._wall.BottomRightCorner.X, j, WallSymbol);
        }
    }

    private Food PrepareRandomFood(Snake snake, Dictionary<Point, Food> foodMap)
    {
        var randomFactoryIndex = Random.Shared.Next(FoodFactories.Length);

        Point randomPosition;
        do
        {
            var randomX = Random.Shared.Next(this._wall.TopLeftCorner.X + 1, this._wall.BottomRightCorner.X);
            var randomY = Random.Shared.Next(this._wall.TopLeftCorner.Y + 1, this._wall.BottomRightCorner.Y);

            randomPosition = new Point(randomX, randomY);
        } while (snake.IntersectsWith(randomPosition) || foodMap.ContainsKey(randomPosition));

        var newFood = FoodFactories[randomFactoryIndex](randomPosition);

        foodMap[newFood.Position] = newFood;
        ConsoleHelper.Write(newFood.Position, newFood.Symbol);

        return newFood;
    }

    private bool AskForRestart()
    {
        const string message = "Would you like to start again? (y/n) ";

        var messageCoordinates = new Point(this._wall.BottomRightCorner.X + 2, this._wall.TopLeftCorner.Y + 5);
        ConsoleHelper.Write(messageCoordinates, message);

        var answer = Console.ReadLine()!;
        ConsoleHelper.Write(messageCoordinates, new string(' ', message.Length + answer.Length));

        return answer == "y";
    }

    private void PrintSpeed(float speed) => ConsoleHelper.Write(this._wall.BottomRightCorner.X + 2, this._wall.TopLeftCorner.Y + 2, $"Speed: {speed:f1}{new string(' ', 5)}");

    private void PrintScore(int score) => ConsoleHelper.Write(this._wall.BottomRightCorner.X + 2, this._wall.TopLeftCorner.Y + 3, $"Score: {score}{new string(' ', 10)}");

    private void PrintGameOver() => ConsoleHelper.WriteLine(this._wall.TopLeftCorner.X, this._wall.BottomRightCorner.Y + 1, "Game over!");
}