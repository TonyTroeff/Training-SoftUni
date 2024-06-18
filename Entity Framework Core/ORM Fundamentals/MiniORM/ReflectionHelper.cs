namespace MiniORM;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

internal static class ReflectionHelper
{
    /// <summary>
    /// Replaces an auto-generated backing field with an object instance.
    /// Commonly used to set properties without a setter.
    /// </summary>
    public static void ReplaceBackingField(object sourceObj, string propertyName, object targetObj)
    {
        var backingField = sourceObj.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField)
            .First(fi => fi.Name == $"<{propertyName}>k__BackingField");

        backingField.SetValue(sourceObj, targetObj);
    }

    /// <summary>
    /// Extension method for MemberInfo, which checks if a member contains an attribute.
    /// </summary>
    public static bool HasAttribute<T>(this MemberInfo mi)
        where T : Attribute
    {
        var hasAttribute = mi.GetCustomAttribute<T>() != null;
        return hasAttribute;
    }

    internal static PropertyInfo[] GetAllowedSqlProperties(this Type type)
    {
        return type.GetProperties().Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType)).ToArray();
    }

    internal static PropertyInfo[] GetKeyProperties(this Type type)
    {
        var keyProperties = type.GetProperties().Where(pi => pi.HasAttribute<KeyAttribute>()).ToArray();
        if (keyProperties.Length == 0)
            throw new InvalidOperationException($"There is no primary key configuration for type {type}.");

        return keyProperties;
    }

    internal static PropertyInfo GetSingleKeyProperty(this Type type)
    {
        var keyProperties = type.GetKeyProperties();
        if (keyProperties.Length != 1)
            throw new InvalidOperationException($"Expected a non-compound primary key for type {type}.");

        return keyProperties[0];
    }
}