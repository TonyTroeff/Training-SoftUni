using DI.Core.Interfaces;
using DI.Demo.Interfaces;
using DI.Demo.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Demo;

internal class Program
{
    static void Main(string[] args)
    {
        // DemoBuiltInDI();
        DemoCustomDI();
    }

    private static void DemoCustomDI()
    {
        IModule module = new DemoModule();
        module.Configure();

        GetAllEntities<Organization>(module);
        GetAllEntities<Group>(module);
        GetAllEntities<User>(module);
    }

    private static void GetAllEntities<T>(IModule module)
    {
        IService<T>? service = module.GetService<IService<T>>();
        ArgumentNullException.ThrowIfNull(service);

        _ = service.GetAll();
    }

    private static void DemoBuiltInDI()
    {
        ServiceCollection serviceCollection = new ServiceCollection();

        // Singleton - One instance for all scopes
        // Scoped - One instance per scope
        // Transient - One instance whenever necessary
        serviceCollection.AddSingleton<IWriter, ConsoleWriter>();
        serviceCollection.AddScoped<IService<User>, DefaultService<User>>();

        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        IService<User> userService = scope.ServiceProvider.GetRequiredService<IService<User>>();
        _ = userService.GetAll();
    }
}
