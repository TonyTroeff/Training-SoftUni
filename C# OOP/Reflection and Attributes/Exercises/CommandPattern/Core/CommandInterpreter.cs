namespace CommandPattern.Core;

using System.Reflection;
using CommandPattern.Core.Contracts;

public class CommandInterpreter : ICommandInterpreter
{
    public string Read(string args)
    {
        var data = args.Split();

        var assembly = Assembly.GetCallingAssembly();
        var commandType = assembly.GetTypes().FirstOrDefault(x => typeof(ICommand).IsAssignableFrom(x) && x.Name.StartsWith(data[0]));
        if (commandType is null) throw new InvalidOperationException("Invalid command");

        var commandObj = Activator.CreateInstance(commandType);
        if (commandObj is ICommand command) return command.Execute(data[1..]);

        return string.Empty;
    }
}