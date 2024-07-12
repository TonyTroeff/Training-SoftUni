namespace Logging.Interfaces;

public interface IAppender
{
    int AppendedMessagesCount { get; }

    void Append(ILogMessage logMessage);
}