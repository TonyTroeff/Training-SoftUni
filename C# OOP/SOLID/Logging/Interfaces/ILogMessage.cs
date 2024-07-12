namespace Logging.Interfaces;

using Logging.Enums;

public interface ILogMessage
{
    ReportLevel ReportLevel { get; }
    string Message { get; }
    DateTime Time { get; }
}