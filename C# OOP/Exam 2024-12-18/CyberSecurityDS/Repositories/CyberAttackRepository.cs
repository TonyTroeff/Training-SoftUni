using CyberSecurityDS.Models.Contracts;

namespace CyberSecurityDS.Repositories;

public class CyberAttackRepository : BaseRepository<ICyberAttack>
{
    protected override string ExtractUniqueKey(ICyberAttack model)
        => model.AttackName;
}
