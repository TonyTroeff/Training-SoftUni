using SimpleSnake.Core;
using SimpleSnake.GameObjects;
using SimpleSnake.Interfaces;
using SimpleSnake.Utilities;

namespace SimpleSnake;

public class StartUp
{
    public static void Main()
    {
        ConsoleWindow.CustomizeConsole();

        ConsoleOperator consoleOperator = new ConsoleOperator();
        Playground playground = new Playground(60, 20);

        IEngine engine = new Engine(playground, consoleOperator, consoleOperator);
        engine.Run();
    }
}
