namespace CommandPattern.Core.Commands;

using CommandPattern.Core.Contracts;

public class ExitCommand : ICommand
{
    public string Execute(string[] args)
    {
        Environment.Exit(0);
        return string.Empty;
    }
}