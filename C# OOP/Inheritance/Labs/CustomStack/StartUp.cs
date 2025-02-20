namespace CustomStack;

public class StartUp
{
    static void Main()
    {
        StackOfStrings stack = new StackOfStrings();
        Console.WriteLine(stack.IsEmpty());

        stack.AddRange(new[] { "first", "second", "third", "hello", "world" });

        Console.WriteLine(stack.IsEmpty());
        Console.WriteLine(string.Join(", ", stack));
    }
}
