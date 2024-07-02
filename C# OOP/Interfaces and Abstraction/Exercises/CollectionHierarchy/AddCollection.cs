namespace CollectionHierarchy;

using System.Collections.Generic;
using CollectionHierarchy.Interfaces;

public class AddCollection : ISupportsAdd
{
    private readonly List<string> _list = new();
    
    public int Add(string item)
    {
        this._list.Add(item);
        return this._list.Count - 1;
    }
}