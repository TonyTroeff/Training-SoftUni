namespace Snake.Utilities;

using System;

public readonly struct Point : IEquatable<Point>
{
    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public int X { get; }
    public int Y { get; }

    public bool Equals(Point other) => this.X == other.X && this.Y == other.Y;

    public override bool Equals(object obj) => obj is Point p && this.Equals(p);

    public override int GetHashCode() => HashCode.Combine(this.X, this.Y);

    public static Point operator +(Point left, Point right) => new(left.X + right.X, left.Y + right.Y);

    public static bool operator ==(Point left, Point right) => left.Equals(right);

    public static bool operator !=(Point left, Point right) => !left.Equals(right);
}