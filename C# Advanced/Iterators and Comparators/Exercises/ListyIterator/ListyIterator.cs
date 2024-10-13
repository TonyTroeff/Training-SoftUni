using System.Collections;

namespace ListyIterator;

public class ListyIterator<T> : IEnumerable<T>
{
    private readonly List<T> _list;
    private int _index;

    public ListyIterator(List<T> list)
    {
        this._list = list ?? throw new ArgumentNullException(nameof(list));
    }

    public bool Move()
    {
        if (!this.HasNext()) return false;

        this._index++;
        return true;
    }

    public bool HasNext() => this._index + 1 < this._list.Count;

    public void Print()
    {
        if (this._index >= this._list.Count)
            throw new InvalidOperationException("Invalid Operation!");
        
        Console.WriteLine(this._list[this._index]);
    }

    // public IEnumerator<T> GetEnumerator() => this._list.GetEnumerator();

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T element in this._list)
            yield return element;
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
