namespace Logging.Appenders;

using Logging.Interfaces;

public class FileAppender : BaseAppender
{
    private readonly string _fileName;

    public FileAppender(string fileName, ILayout layout, Func<ILogMessage, bool>? filter = null)
        : base(layout, filter)
    {
        this._fileName = fileName;
    }

    protected override void Append(string formattedLogMessage)
    {
        File.AppendAllLines(this._fileName, new[] { formattedLogMessage });
    }
}