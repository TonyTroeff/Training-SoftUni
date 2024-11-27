namespace TheContentDepartment.Repositories;

using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

public class MemberRepository : IRepository<ITeamMember>
{
    private readonly List<ITeamMember> _models;

    public MemberRepository()
    {
        this._models = new List<ITeamMember>();
        this.Models = this._models.AsReadOnly();
    }
    
    public IReadOnlyCollection<ITeamMember> Models { get; }
    
    public void Add(ITeamMember model) => this._models.Add(model);

    public ITeamMember TakeOne(string modelName) => this.Models.FirstOrDefault(m => m.Name == modelName);
}