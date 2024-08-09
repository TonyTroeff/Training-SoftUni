namespace Snake.GameObjects;

using System.Collections.Generic;
using global::Snake.Utilities;

public class Snake
{
    private readonly Queue<Point> _queue = new();
    private readonly Dictionary<Point, int> _dict = new();

    public Snake(Point head, Point direction)
    {
        this.Head = head;
        this.Direction = direction;
        this._queue.Enqueue(this.Head);
        this._dict[head] = 1;
    }

    public Point Head { get; private set; }
    public Point Direction { get; set; }
    public int Length => this._queue.Count;

    public Point GrowFront()
    {
        var newHead = this.Head + this.Direction;

        this.Head = newHead;
        this._queue.Enqueue(newHead);

        this._dict.TryGetValue(newHead, out var count);
        this._dict[newHead] = count + 1;

        return newHead;
    }

    public Point ShortenBack()
    {
        var tailElement = this._queue.Dequeue();

        if (--this._dict[tailElement] == 0)
            this._dict.Remove(tailElement);

        return tailElement;
    }

    public bool IntersectsWith(Point p, bool ignoreHead = false)
    {
        this._dict.TryGetValue(p, out var count);
        if (ignoreHead && p == this.Head) count--;

        return count > 0;
    }
}
