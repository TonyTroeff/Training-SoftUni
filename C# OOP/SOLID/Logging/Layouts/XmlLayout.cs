namespace Logging.Layouts;

using System.Text;
using Logging.Interfaces;

public class XmlLayout : ILayout
{
    public string Format(ILogMessage logMessage)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("<log>");
        stringBuilder.AppendLine($"  <date>{logMessage.Time}</date>");
        stringBuilder.AppendLine($"  <level>{logMessage.ReportLevel}</level>");
        stringBuilder.AppendLine($"  <message>{logMessage.Message}</message>");
        stringBuilder.Append("</log>");

        return stringBuilder.ToString();
    }
}