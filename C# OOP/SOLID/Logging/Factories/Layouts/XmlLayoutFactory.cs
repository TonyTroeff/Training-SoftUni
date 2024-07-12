namespace Logging.Factories.Layouts;

using Logging.Interfaces;
using Logging.Interfaces.Factories;
using Logging.Layouts;

public class XmlLayoutFactory : ILayoutFactory
{
    public ILayout CreateLayout() => new XmlLayout();
}