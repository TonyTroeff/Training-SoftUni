namespace MilitaryElite.Interfaces;

using System.Collections.Generic;

public interface IEngineer : ISpecializedSoldier
{
    IReadOnlyCollection<IRepair> Repairs { get; }
}