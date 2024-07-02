namespace CollectionHierarchy;

using System.Collections.Generic;
using CollectionHierarchy.Interfaces;

public class AddRemoveCollection : ISupportsAdd, ISupportsRemove
{
    private readonly Queue<string> _queue = new();
    
    public int Add(string item)
    {
        this._queue.Enqueue(item);
        return 0;
    }

    public string Remove() => this._queue.Dequeue();
}