namespace MilitaryElite.Interfaces;

using System.Collections.Generic;

public interface ILieutenantGeneral : IPrivateSoldier
{
    IReadOnlyCollection<ISoldier> Privates { get; }
}