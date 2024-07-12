namespace Logging.Appenders;

using Logging.Interfaces;

public abstract class BaseAppender : IAppender
{
    private readonly ILayout _layout;
    private readonly Func<ILogMessage, bool>? _filter;

    protected BaseAppender(ILayout layout, Func<ILogMessage, bool>? filter = null)
    {
        this._layout = layout;
        this._filter = filter;
    }

    public int AppendedMessagesCount { get; private set; }

    public void Append(ILogMessage logMessage)
    {
        // 1. Check report level
        // - delegate
        // - new interface
        if (this._filter is not null && !this._filter(logMessage)) return;

        // 2. Format the log message
        var formattedLogMessage = this._layout.Format(logMessage);

        // 3. Append the message
        this.Append(formattedLogMessage);

        this.AppendedMessagesCount++;
    }

    protected abstract void Append(string formattedLogMessage);
}