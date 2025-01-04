using DI.Core.Attributes;
using DI.Core.Interfaces;
using System.Reflection;

namespace DI.Core;

public abstract class AbstractModule : IModule
{
    private const BindingFlags MemberSearchBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    private readonly Dictionary<Type, Type> _serviceTypesMap = new Dictionary<Type, Type>();
    private readonly Dictionary<Type, object?> _instancesCache = new Dictionary<Type, object?>();

    public abstract void Configure();

    public object? GetService(Type serviceType)
        => this.GetService(serviceType, new HashSet<Type>());

    public T? GetService<T>()
    {
        object? service = this.GetService(typeof(T));

        if (service is T typedService) return typedService;
        return default;
    }

    protected void RegisterService<TInterface, TImplementation>()
        where TImplementation : TInterface
        => this.RegisterService(typeof(TInterface), typeof(TImplementation));

    protected void RegisterService(Type interfaceType, Type implementationType)
    {
        if (!implementationType.IsClass || implementationType.IsAbstract)
            throw new InvalidOperationException("Implementation type is not an active class.");
        if (!implementationType.IsAssignableTo(interfaceType))
            throw new InvalidOperationException("Implementation type does not inherit the provided service type.");
        if (this._serviceTypesMap.ContainsKey(interfaceType))
            throw new InvalidOperationException($"This module already has a registration for service of type {interfaceType.Name}");

        this._serviceTypesMap[interfaceType] = implementationType;
    }

    private object? GetService(Type serviceType, HashSet<Type> cycleDetector)
    {
        if (!this._serviceTypesMap.TryGetValue(serviceType, out Type? implementationType))
            return default;

        if (this._instancesCache.TryGetValue(implementationType, out object? cachedResult))
            return cachedResult;

        object? serviceInstance = this.Instantiate(implementationType, cycleDetector);
        this._instancesCache[implementationType] = serviceInstance;

        if (serviceInstance is not null)
        {
            this.InjectProperties(implementationType, serviceInstance, cycleDetector);
            this.InjectFields(implementationType, serviceInstance, cycleDetector);
        }

        return serviceInstance;
    }

    private object? Instantiate(Type implementationType, HashSet<Type> cycleDetector)
    {
        if (!cycleDetector.Add(implementationType))
            throw new InvalidOperationException("Cycle dependencies were detected.");

        ConstructorInfo[] constructors = implementationType.GetConstructors();
        if (constructors.Length > 1) throw new InvalidOperationException("Too many constructors - we can't choose one.");

        ConstructorInfo constructor = constructors[0];
        ParameterInfo[] parameters = constructor.GetParameters();
        object?[] dependencies = new object?[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            // NOTE: Handle optional/required parameters differently.
            dependencies[i] = this.GetService(parameters[i].ParameterType, cycleDetector);
        }

        object? result = constructor.Invoke(dependencies);
        cycleDetector.Remove(implementationType);

        return result;
    }

    private void InjectProperties(Type implementationType, object instance, HashSet<Type> cycleDetector)
    {
        PropertyInfo[] properties = implementationType.GetProperties(MemberSearchBindingFlags);

        foreach (PropertyInfo property in properties.Where(p => p.CanWrite))
        {
            InjectAttribute? injectAttribute = property.GetCustomAttribute<InjectAttribute>();
            if (injectAttribute is null) continue;

            object? dependency = this.GetService(property.PropertyType, cycleDetector);
            property.SetValue(instance, dependency);
        }
    }

    private void InjectFields(Type implementationType, object instance, HashSet<Type> cycleDetector)
    {
        FieldInfo[] fields = implementationType.GetFields(MemberSearchBindingFlags);

        foreach (FieldInfo field in fields)
        {
            InjectAttribute? injectAttribute = field.GetCustomAttribute<InjectAttribute>();
            if (injectAttribute is null) continue;

            object? dependency = this.GetService(field.FieldType, cycleDetector);
            field.SetValue(instance, dependency);
        }
    }
}
