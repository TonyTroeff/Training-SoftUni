namespace Stealer;

using System.Reflection;

public class Spy
{
    public string StealFieldInfo(string className, params string[] fieldNames)
    {
        var type = Type.GetType(className);
        if (type is null) throw new InvalidOperationException("Invalid class name");

        var fields = new List<FieldInfo>();
        foreach (var fieldName in fieldNames)
        {
            var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            if (field is null) throw new InvalidOperationException($"Field called \"{fieldName}\" was not found");
            
            fields.Add(field);
        }

        var output = new List<string> { $"Class under investigation: {type.FullName}" };

        var instance = Activator.CreateInstance(type);
        foreach (var field in fields)
        {
            var value = field.GetValue(instance);
            output.Add($"{field.Name} = {value}");
        }

        return string.Join(Environment.NewLine, output);
    }

    public string AnalyzeAccessModifiers(string className)
    {
        var errors = new List<string>();
        
        var type = Type.GetType(className);
        if (type is null) throw new InvalidOperationException("Invalid class name");
        
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
        foreach (var field in fields)
            errors.Add($"{field.Name} must be private!");

        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var property in properties)
        {
            var getMethod = property.GetMethod;
            if (getMethod is not null && !getMethod.IsPublic)
                errors.Add($"{getMethod.Name} have to be public!");
        }
        
        foreach (var property in properties)
        {
            var setMethod = property.SetMethod;
            if (setMethod is not null && !setMethod.IsPrivate)
                errors.Add($"{setMethod.Name} have to be private!");
        }

        return string.Join(Environment.NewLine, errors);
    }

    public string RevealPrivateMethods(string className)
    {
        var output = new List<string>();
        
        var type = Type.GetType(className);
        if (type is null) throw new InvalidOperationException("Invalid class name");
        
        output.Add($"All Private Methods of Class: {type.FullName}");
        if (type.BaseType is not null) output.Add($"Base Class: {type.BaseType.Name}");
        
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (var method in methods)
            output.Add(method.Name);

        return string.Join(Environment.NewLine, output);
    }

    public string CollectGettersAndSetters(string className)
    {
        var output = new List<string>();
        
        var type = Type.GetType(className);
        if (type is null) throw new InvalidOperationException("Invalid class name");
        
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var property in properties)
        {
            var getMethod = property.GetMethod;
            if (getMethod is not null) output.Add($"{getMethod.Name} will return {property.PropertyType.FullName}!");
        }
        
        foreach (var property in properties)
        {
            var setMethod = property.SetMethod;
            if (setMethod is not null) output.Add($"{setMethod.Name} will set field of {property.PropertyType.FullName}!");
        }

        return string.Join(Environment.NewLine, output);
    }
}