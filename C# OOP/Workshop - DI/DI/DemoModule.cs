using DI.Core;
using DI.Demo.Interfaces;
using DI.Demo.Models;

namespace DI.Demo;

public class DemoModule : AbstractModule
{
    public override void Configure()
    {
        this.RegisterService<IWriter, ConsoleWriter>();
        this.RegisterService<IService<Organization>, DefaultService<Organization>>();
        this.RegisterService<IService<Group>, GroupService>();
        this.RegisterService<IService<User>, UserService>();
    }
}
