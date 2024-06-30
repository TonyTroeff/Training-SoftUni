namespace MilitaryElite.Interfaces;

using System.Collections.Generic;

public interface ICommando : ISpecializedSoldier
{
    IReadOnlyCollection<IMission> Missions { get; }
}