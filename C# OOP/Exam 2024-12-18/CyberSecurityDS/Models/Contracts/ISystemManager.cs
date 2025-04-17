using CyberSecurityDS.Repositories.Contracts;

namespace CyberSecurityDS.Models.Contracts;

public interface ISystemManager
{
    IRepository<ICyberAttack> CyberAttacks { get; }
    IRepository<IDefensiveSoftware> DefensiveSoftwares { get; }
}
