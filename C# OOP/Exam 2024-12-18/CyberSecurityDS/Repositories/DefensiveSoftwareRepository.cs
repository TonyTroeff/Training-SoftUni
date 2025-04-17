using CyberSecurityDS.Models.Contracts;

namespace CyberSecurityDS.Repositories;

public class DefensiveSoftwareRepository : BaseRepository<IDefensiveSoftware>
{
    protected override string ExtractUniqueKey(IDefensiveSoftware model)
        => model.Name;
}
