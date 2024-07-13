namespace CommandPattern.Core;

using CommandPattern.Core.Contracts;

public class Engine : IEngine
{
    private readonly ICommandInterpreter _commandInterpreter;

    public Engine(ICommandInterpreter commandInterpreter)
    {
        this._commandInterpreter = commandInterpreter ?? throw new ArgumentNullException(nameof(commandInterpreter));
    }

    public void Run()
    {
        var input = Console.ReadLine();
        while (!string.IsNullOrWhiteSpace(input))
        {
            var output = this._commandInterpreter.Read(input!);
            Console.WriteLine(output);

            input = Console.ReadLine();
        }
    }
}