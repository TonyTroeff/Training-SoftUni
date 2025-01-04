using DI.Core.Attributes;
using DI.Demo.Interfaces;
using DI.Demo.Models;

namespace DI.Demo;

public class UserService : IService<User>
{
    [Inject]
    private readonly IService<Group> _groupService = null!;

    [Inject]
    private IWriter Writer { get; init; } = null!;

    public User[] GetAll()
    {
        this.Writer.Write($"User Service, Get All was invoked!");
        return Array.Empty<User>();
    }
}
