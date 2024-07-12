namespace Logging.Models;

using Logging.Enums;
using Logging.Interfaces;

public class LogMessage : ILogMessage
{
    public LogMessage(ReportLevel reportLevel, string message, DateTime time)
    {
        this.ReportLevel = reportLevel;
        this.Message = message;
        this.Time = time;
    }

    public ReportLevel ReportLevel { get; }
    public string Message { get; }
    public DateTime Time { get; }
}