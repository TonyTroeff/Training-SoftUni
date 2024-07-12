namespace Logging.Interfaces.Factories;

public interface IAppenderFactory
{
    IAppender CreateAppender(ILayout layout, Func<ILogMessage, bool>? filter = null);
}