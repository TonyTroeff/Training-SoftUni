using System.Collections;

namespace List;

public class CustomList<TValue> : IEnumerable<TValue>
{
    private const int DefaultCapacity = 4;

    private TValue[] _buffer;

    public CustomList()
        : this(DefaultCapacity)
    {
    }

    public CustomList(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("The initial capacity must be a positive integer.", nameof(capacity));

        this._buffer = new TValue[capacity];
    }

    public int Count { get; private set; }

    public TValue this[int index]
    {
        get
        {
            this.ValidateIndex(index);
            return this._buffer[index];
        }

        set
        {
            this.ValidateIndex(index);
            this._buffer[index] = value;
        }
    }

    public void Add(TValue element)
    {
        if (this.Count == this._buffer.Length) this.Grow();

        this._buffer[this.Count++] = element;
    }

    public void Insert(int index, TValue element)
    {
        if (index == this.Count) this.Add(element);
        else
        {
            this.ValidateIndex(index);

            if (this.Count == this._buffer.Length) this.Grow();

            Array.Copy(this._buffer, index, this._buffer, index + 1, this.Count - index);
            // for (int i = this.Count - 1; i >= index; i--)
            //     this._buffer[i + 1] = this._buffer[i];

            this._buffer[index] = element;
            this.Count++;
        }
    }

    public TValue RemoveAt(int index)
    {
        this.ValidateIndex(index);

        TValue removedElement = this._buffer[index];

        Array.Copy(this._buffer, index + 1, this._buffer, index, this.Count - (index + 1));
        // for (int i = index + 1; i < this.Count; i++)
        //     this._buffer[i - 1] = this._buffer[i];

        this._buffer[--this.Count] = default!;
        return removedElement;
    }
    
    public bool Contains(TValue element)
    {
        for (int i = 0; i < this.Count; i++)
        {
            if (EqualityComparer<TValue>.Default.Equals(this._buffer[i], element))
                return true;
        }

        return false;
    }
    
    public void Swap(int firstIndex, int secondIndex)
    {
        this.ValidateIndex(firstIndex);
        this.ValidateIndex(secondIndex);

        if (firstIndex != secondIndex)
        {
            TValue swap = this._buffer[firstIndex];
            this._buffer[firstIndex] = this._buffer[secondIndex];
            this._buffer[secondIndex] = swap;
        }
    }

    private void Grow()
    {
        TValue[] newBuffer = new TValue[this._buffer.Length * 2];

        Array.Copy(this._buffer, newBuffer, this.Count);

        // for (int i = 0; i < this.Count; i++)
        //     newBuffer[i] = this._buffer[i];

        this._buffer = newBuffer;
    }

    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= this.Count)
            throw new IndexOutOfRangeException($"Index must be in the range [0, {this.Count})");
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        for (int i = 0; i < this.Count; i++)
            yield return this._buffer[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
