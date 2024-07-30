namespace TheContentDepartment.Repositories;

using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

public class ResourceRepository : IRepository<IResource>
{
    private readonly List<IResource> _models;

    public ResourceRepository()
    {
        this._models = new List<IResource>();
        this.Models = this._models.AsReadOnly();
    }
    
    public IReadOnlyCollection<IResource> Models { get; }

    public void Add(IResource model) => this._models.Add(model);

    public IResource TakeOne(string modelName) => this.Models.FirstOrDefault(m => m.Name == modelName);
}