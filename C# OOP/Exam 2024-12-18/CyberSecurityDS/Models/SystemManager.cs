using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Repositories;
using CyberSecurityDS.Repositories.Contracts;

namespace CyberSecurityDS.Models;

public class SystemManager : ISystemManager
{
    public IRepository<ICyberAttack> CyberAttacks { get; }
    public IRepository<IDefensiveSoftware> DefensiveSoftwares { get; }

    public SystemManager()
    {
        this.CyberAttacks = new CyberAttackRepository();
        this.DefensiveSoftwares = new DefensiveSoftwareRepository();
    }
}
