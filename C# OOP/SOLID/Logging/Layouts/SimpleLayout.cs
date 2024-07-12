namespace Logging.Layouts;

using Logging.Interfaces;

public class SimpleLayout : ILayout
{
    public string Format(ILogMessage logMessage)
        => $"{logMessage.Time} - {logMessage.ReportLevel} - {logMessage.Message}";
}