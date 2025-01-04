using DI.Core.Attributes;
using DI.Demo.Interfaces;
using DI.Demo.Models;

namespace DI.Demo;

public class GroupService : IService<Group>
{
    [Inject]
    private readonly IService<User> _userService = null!;

    [Inject]
    private readonly IWriter _writer = null!;

    public Group[] GetAll()
    {
        this._writer.Write($"Group Service, Get All was invoked!");
        return Array.Empty<Group>();
    }
}
