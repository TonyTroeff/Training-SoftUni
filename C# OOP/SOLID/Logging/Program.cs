namespace Logging;

using System.Globalization;
using Logging.Appenders;
using Logging.Enums;
using Logging.Factories.Appenders;
using Logging.Factories.Layouts;
using Logging.Interfaces;
using Logging.Interfaces.Factories;
using Logging.Layouts;
using Logging.Loggers;

internal class Program
{
    private static readonly Dictionary<string, ILayoutFactory> _layoutFactories = CreateLayoutFactories();
    private static readonly Dictionary<string, IAppenderFactory> _appenderFactories = CreateAppenderFactories();

    static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());

        var appenders = new IAppender[n];
        for (var i = 0; i < n; i++)
        {
            var appenderData = Console.ReadLine().Split();
            appenders[i] = BuildAppender(appenderData);
        }

        ILogger logger = new Logger(appenders);

        var logMessageInput = Console.ReadLine();
        while (logMessageInput != "END")
        {
            var logMessageData = logMessageInput.Split('|');

            var reportLevel = Enum.Parse<ReportLevel>(logMessageData[0], true);
            var time = DateTime.Parse(logMessageData[1], CultureInfo.InvariantCulture);
            var message = logMessageData[2];

            logger.Log(reportLevel, message, time);

            logMessageInput = Console.ReadLine();
        }

        for (var i = 0; i < n; i++)
        {
            Console.WriteLine($"Appender #{i + 1} -> Messages count: {appenders[i].AppendedMessagesCount}");
        }
    }

    private static Dictionary<string, ILayoutFactory> CreateLayoutFactories()
        => new Dictionary<string, ILayoutFactory>
        {
            [nameof(SimpleLayout)] = new SimpleLayoutFactory(),
            [nameof(XmlLayout)] = new XmlLayoutFactory()
        };

    private static Dictionary<string, IAppenderFactory> CreateAppenderFactories()
        => new Dictionary<string, IAppenderFactory>
        {
            [nameof(ConsoleAppender)] = new ConsoleAppenderFactory(),
            [nameof(FileAppender)] = new FileAppenderFactory()
        };

    private static IAppender BuildAppender(string[] data)
    {
        var layoutType = data[1];
        var layout = _layoutFactories[layoutType].CreateLayout();

        var appenderType = data[0];
        Func<ILogMessage, bool>? filter = null;

        if (data.Length == 3)
        {
            var reportLevelThreshold = Enum.Parse<ReportLevel>(data[2], true);
            filter = (lm) => lm.ReportLevel >= reportLevelThreshold;
        }

        return _appenderFactories[appenderType].CreateAppender(layout, filter);
    }
}