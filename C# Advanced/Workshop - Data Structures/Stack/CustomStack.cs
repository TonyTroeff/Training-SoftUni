using System.Collections;

namespace Stack;

public class CustomStack<TValue> : IEnumerable<TValue>
{
    private const int DefaultCapacity = 4;

    private TValue[] _buffer;

    public CustomStack()
        : this(DefaultCapacity)
    {
    }

    public CustomStack(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("The initial capacity must be a positive integer.", nameof(capacity));

        this._buffer = new TValue[capacity];
    }

    public int Count { get; private set; }

    public void Push(TValue element)
    {
        if (this.Count == this._buffer.Length) this.Grow();
        this._buffer[this.Count++] = element;
    }

    public TValue Pop()
    {
        this.ValidateNotEmpty();

        TValue removedElement = this._buffer[this.Count - 1];
        this._buffer[--this.Count] = default!;

        return removedElement;
    }

    public TValue Peek()
    {
        this.ValidateNotEmpty();
        return this._buffer[this.Count - 1];
    }

    public void Clear()
    {
        Array.Clear(this._buffer);
        this.Count = 0;
    }

    public void ForEach(Action<TValue> action)
    {
        for (int i = this.Count - 1; i >= 0; i--)
            action(this._buffer[i]);
    }

    private void Grow()
    {
        TValue[] newBuffer = new TValue[this._buffer.Length * 2];
        Array.Copy(this._buffer, newBuffer, this.Count);

        this._buffer = newBuffer;
    }

    private void ValidateNotEmpty()
    {
        if (this.Count == 0) throw new InvalidOperationException("Stack is empty.");
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        for (int i = this.Count - 1; i >= 0; i--)
            yield return this._buffer[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
