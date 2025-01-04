using SimpleSnake.Enums;
using SimpleSnake.GameObjects;
using SimpleSnake.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SimpleSnake.Core;

public class Engine : IEngine
{
    private const char WallSymbol = '\u25A0';
    private const char SnakeSymbol = '\u25CF';
    private const int FoodElementsCount = 5;

    private const int MinWaitTime = 10;
    private const float SpeedIncrease = 0.1f;

    private static readonly Dictionary<Direction, Point> _directions = new Dictionary<Direction, Point>
    {
        [Direction.Up] = new Point(0, -1),
        [Direction.Right] = new Point(1, 0),
        [Direction.Down] = new Point(0, 1),
        [Direction.Left] = new Point(-1, 0)
    };

    private static readonly Func<Point, Food>[] _foodFactories =
    {
        p => new Food(p, 1, '*'),
        p => new Food(p, 2, '$'),
        p => new Food(p, 3, '#')
    };

    private readonly Playground _playground;
    private readonly IWriter _writer;
    private readonly IReader _reader;

    public Engine(Playground playground, IWriter writer, IReader reader)
    {
        this._playground = playground ?? throw new ArgumentNullException(nameof(playground));
        this._writer = writer ?? throw new ArgumentNullException(nameof(writer));
        this._reader = reader ?? throw new ArgumentNullException(nameof(reader));
    }

    public void Run()
    {
        this.WritePlayground();

        bool shouldPlay = true;
        while (shouldPlay)
            shouldPlay = this.Play();
    }

    public bool Play()
    {
        Snake snake = this.PrepareSnake();
        Dictionary<Point, Food> foodMap = this.PrepareFood(snake);

        int score = 0;
        float waitTime = 100;
        this.WriteScoreLabel(score);

        while (true)
        {
            this.TryChangeDirection(snake);
            if (!this.MoveSnake(snake))
            {
                this.WriteRestartMessage();

                bool shouldRestart = this._reader.ReadConfirmation('y');
                this.ClearRestartMessage();

                if (shouldRestart)
                {
                    this.ClearGameObjects(snake, foodMap);
                }
                else
                {
                    this.WriteEndGameMessage();
                }

                return shouldRestart;
            }

            if (foodMap.TryGetValue(snake.Head, out Food consumedFood))
            {
                foodMap.Remove(snake.Head);
                score += consumedFood.Score;
                this.WriteScoreLabel(score);
                this.AddNewFood(snake, foodMap);
            }
            else
            {
                this._writer.Write(snake.Shorten(), ' ');
            }

            Thread.Sleep((int) waitTime);
            if (waitTime > MinWaitTime) waitTime -= SpeedIncrease;
        }
    }

    private bool TryChangeDirection(Snake snake)
    {
        if (!this._reader.TryReadDirection(out Direction? direction)
            || !_directions.TryGetValue(direction.Value, out Point vector))
            return false;

        bool hasChanges = vector != -snake.Direction;
        if (hasChanges) snake.Direction = vector;

        return hasChanges;
    }

    private bool MoveSnake(Snake snake)
    {
        if (!snake.Grow() || !IsWithinActivePlayground(snake.Head))
            return false;

        this._writer.Write(snake.Head, SnakeSymbol);

        return true;
    }

    private bool IsWithinActivePlayground(Point point)
        => point.X >= 1 && point.X <= this._playground.Width
            && point.Y >= 1 && point.Y <= this._playground.Height;

    private Snake PrepareSnake()
    {
        Point defaultDirection = _directions[Direction.Right];
        Snake snake = new Snake(new Point(1, 1), defaultDirection);
        this._writer.Write(snake.Head, SnakeSymbol);

        for (int i = 1; i < 5; i++)
        {
            snake.Grow();
            this._writer.Write(snake.Head, SnakeSymbol);
        }

        return snake;
    }

    private Dictionary<Point, Food> PrepareFood(Snake snake)
    {
        Dictionary<Point, Food> foodMap = new Dictionary<Point, Food>();

        for (int i = 0; i < FoodElementsCount; i++)
            this.AddNewFood(snake, foodMap);

        return foodMap;
    }

    private void AddNewFood(Snake snake, Dictionary<Point, Food> foodMap)
    {
        Food food = GenerateRandomFood(snake, foodMap);
        foodMap[food.Position] = food;
        this._writer.Write(food.Position, food.Symbol);
    }

    private Food GenerateRandomFood(Snake snake, Dictionary<Point, Food> foodMap)
    {
        int factoryIndex = Random.Shared.Next(_foodFactories.Length);

        Point randomPosition;
        do
        {
            randomPosition = GenerateRandomPoint();
        }
        while (snake.HasBodyElementAt(randomPosition) || foodMap.ContainsKey(randomPosition));

        return _foodFactories[factoryIndex](randomPosition);
    }

    private Point GenerateRandomPoint()
    {
        int randomX = Random.Shared.Next(this._playground.Width) + 1;
        int randomY = Random.Shared.Next(this._playground.Height) + 1;

        return new Point(randomX, randomY);
    }

    private void ClearGameObjects(Snake snake, Dictionary<Point, Food> foodMap)
    {
        while (snake.BodyLength > 1) this._writer.Write(snake.Shorten(), ' ');
        if (this.IsWithinActivePlayground(snake.Head))
            this._writer.Write(snake.Head, ' ');

        foreach (Point point in foodMap.Keys) this._writer.Write(point, ' ');
    }

    private void WriteScoreLabel(int score)
    {
        Point messageStart = new Point(this._playground.Width + 3, 1);
        this._writer.Write(messageStart, $"Score: {score}{new string(' ', 10)}");
    }

    private void WriteRestartMessage()
    {
        Point messageStart = new Point(this._playground.Width + 3, 4);
        this._writer.Write(messageStart, $"Game over. If you want to restart, press Y.");
    }

    private void ClearRestartMessage()
    {
        Point messageStart = new Point(this._playground.Width + 3, 4);
        this._writer.Write(messageStart, new string(' ', 50));
    }

    private void WriteEndGameMessage()
    {
        Point messageStart = new Point(0, this._playground.Height + 3);
        this._writer.Write(messageStart, "Thank you for playing!");
    }

    private void WritePlayground()
    {
        for (int i = 0; i <= this._playground.Width + 1; i++)
        {
            this._writer.Write(new Point(i, 0), WallSymbol);
            this._writer.Write(new Point(i, this._playground.Height + 1), WallSymbol);
        }

        for (int i = 1; i <= this._playground.Height; i++)
        {
            this._writer.Write(new Point(0, i), WallSymbol);
            this._writer.Write(new Point(this._playground.Width + 1, i), WallSymbol);
        }
    }
}
