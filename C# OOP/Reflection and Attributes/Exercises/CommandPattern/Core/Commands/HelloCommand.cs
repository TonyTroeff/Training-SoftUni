namespace CommandPattern.Core.Commands;

using CommandPattern.Core.Contracts;

public class HelloCommand : ICommand
{
    public string Execute(string[] args)
    {
        if (args.Length != 1) throw new InvalidOperationException("A single argument is required for the \"Hello\" command");
        return $"Hello, {args[0]}";
    }
}