using System.Collections;

namespace LinkedList;

public class CustomDoublyLinkedList<TValue> : IEnumerable<TValue>
{
    private readonly Node _head, _tail;

    public CustomDoublyLinkedList()
    {
        this._head = new Node(default!);
        this._tail = new Node(default!);

        this._head.Next = this._tail;
        this._tail.Prev = this._head;
    }

    public int Count { get; private set; }

    public void AddFirst(TValue value)
    {
        Node newNode = new Node(value);

        newNode.Next = this._head.Next;
        this._head.Next!.Prev = newNode;

        newNode.Prev = this._head;
        this._head.Next = newNode;

        this.Count++;
    }

    public void AddLast(TValue value)
    {
        Node newNode = new Node(value);

        newNode.Prev = this._tail.Prev;
        this._tail.Prev!.Next = newNode;

        newNode.Next = this._tail;
        this._tail.Prev = newNode;

        this.Count++;
    }

    public TValue RemoveFirst()
    {
        this.ValidateNotEmpty();

        Node nodeToDelete = this._head.Next!;
        
        nodeToDelete.Next!.Prev = this._head;
        this._head.Next = nodeToDelete.Next;

        nodeToDelete.Next = null;
        nodeToDelete.Prev = null;

        this.Count--;

        return nodeToDelete.Value;
    }

    public TValue RemoveLast()
    {
        this.ValidateNotEmpty();

        Node nodeToDelete = this._tail.Prev!;

        nodeToDelete.Prev!.Next = this._tail;
        this._tail.Prev = nodeToDelete.Prev;

        nodeToDelete.Next = null;
        nodeToDelete.Prev = null;

        this.Count--;

        return nodeToDelete.Value;
    }

    public void ForEach(Action<TValue> action)
    {
        Node iter = this._head.Next!;

        while (iter != this._tail)
        { 
            action(iter.Value);
            iter = iter.Next!;
        }
    }

    public TValue[] ToArray()
    {
        TValue[] result = new TValue[this.Count];

        Node iter = this._head.Next!;
        for (int i = 0; i < this.Count; i++, iter = iter.Next!)
            result[i] = iter.Value;

        return result;
    }

    private void ValidateNotEmpty()
    {
        if (this.Count == 0) throw new InvalidOperationException("The list is empty");
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        Node iter = this._head.Next!;
        for (int i = 0; i < this.Count; i++, iter = iter.Next!)
            yield return iter.Value;
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private class Node
    {
        public Node(TValue value) => this.Value = value;

        public TValue Value { get; }
        public Node? Prev { get; set; }
        public Node? Next { get; set; }
    }
}
