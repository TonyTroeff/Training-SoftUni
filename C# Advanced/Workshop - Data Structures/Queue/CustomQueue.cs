using System.Collections;

namespace Queue;

public class CustomQueue<TValue> : IEnumerable<TValue>
{
    private const int DefaultCapacity = 4;

    private TValue[] _buffer;
    private int _start;

    public CustomQueue()
        : this(DefaultCapacity)
    {
    }

    public CustomQueue(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("The initial capacity must be a positive integer.", nameof(capacity));

        this._buffer = new TValue[capacity];
    }

    public int Count { get; private set; }

    public void Enqueue(TValue element)
    {
        if (this.Count == this._buffer.Length) this.Grow();

        int index = this.GetBufferPosition(this.Count);
        this._buffer[index] = element;

        this.Count++;
    }

    public TValue Dequeue()
    {
        this.ValidateNotEmpty();

        TValue removedElement = this._buffer[this._start];
        this._buffer[this._start] = default!;

        this.Count--;
        if (this.Count == 0) this._start = 0;
        else this._start = this.GetBufferPosition(1);

        return removedElement;
    }

    public TValue Peek()
    {
        this.ValidateNotEmpty();
        return this._buffer[this._start];
    }

    public void Clear()
    {
        Array.Clear(this._buffer);
        this._start = 0;
        this.Count = 0;
    }

    public void ForEach(Action<TValue> action)
    {
        for (int i = 0; i < this.Count; i++)
            action(this._buffer[this.GetBufferPosition(i)]);
    }

    private void Grow()
    {
        TValue[] newBuffer = new TValue[this._buffer.Length * 2];

        int tailCount = this.Count - this._start;
        Array.Copy(this._buffer, this._start, newBuffer, 0, tailCount);
        Array.Copy(this._buffer, 0, newBuffer, tailCount, this._start);

        this._start = 0;
        this._buffer = newBuffer;
    }

    private void ValidateNotEmpty()
    {
        if (this.Count == 0) throw new InvalidOperationException("Queue is empty.");
    }

    private int GetBufferPosition(int offset)
        => (this._start + offset) % this._buffer.Length;

    public IEnumerator<TValue> GetEnumerator()
    {
        for (int i = 0; i < this.Count; i++)
        {
            int position = this.GetBufferPosition(i);
            yield return this._buffer[position];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
