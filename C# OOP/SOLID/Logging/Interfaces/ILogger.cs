namespace Logging.Interfaces;

using Logging.Enums;

public interface ILogger
{
    void Log(ReportLevel reportLevel, string message, DateTime time);

    void Info(string message, DateTime time)
        => this.Log(ReportLevel.Info, message, time);

    void Warning(string message, DateTime time)
        => this.Log(ReportLevel.Warning, message, time);

    void Error(string message, DateTime time)
        => this.Log(ReportLevel.Error, message, time);

    void Critical(string message, DateTime time)
        => this.Log(ReportLevel.Critical, message, time);

    void Fatal(string message, DateTime time)
        => this.Log(ReportLevel.Fatal, message, time);
}