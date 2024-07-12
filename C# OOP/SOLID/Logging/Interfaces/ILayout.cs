namespace Logging.Interfaces;

public interface ILayout
{
    string Format(ILogMessage logMessage);
}