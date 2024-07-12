namespace Logging.Loggers;

using Logging.Enums;
using Logging.Interfaces;
using Logging.Models;

public class Logger : ILogger
{
    private readonly IAppender[] _appenders;

    public Logger(params IAppender[] appenders)
    {
        this._appenders = appenders;
    }

    public void Log(ReportLevel reportLevel, string message, DateTime time)
    {
        ILogMessage logMessage = new LogMessage(reportLevel, message, time);
        foreach (var appender in this._appenders)
            appender.Append(logMessage);
    }
}