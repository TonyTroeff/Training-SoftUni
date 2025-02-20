namespace CustomStack;

public class StackOfStrings : Stack<string>
{
    public bool IsEmpty()
        => this.Count == 0;

    public void AddRange(IEnumerable<string> values)
    {
        foreach (string val in values)
            this.Push(val);
    }
}
