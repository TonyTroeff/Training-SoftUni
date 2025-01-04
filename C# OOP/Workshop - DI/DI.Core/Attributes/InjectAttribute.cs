namespace DI.Core.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class InjectAttribute : Attribute
{
}
