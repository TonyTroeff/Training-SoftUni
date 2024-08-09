namespace Snake;

using Snake.Core;
using Snake.GameObjects;
using Snake.Utilities;

public static class StartUp
{
    public static void Main()
    {
        ConsoleHelper.CustomizeConsole();

        var wall = new Wall(60, 20);
        
        var engine = new Engine(wall);
        engine.Run();
    }
}