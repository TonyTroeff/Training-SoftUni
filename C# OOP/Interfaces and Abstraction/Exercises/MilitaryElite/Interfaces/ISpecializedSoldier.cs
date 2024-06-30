namespace MilitaryElite.Interfaces;

using MilitaryElite.Enums;

public interface ISpecializedSoldier : IPrivateSoldier
{
    SoldierCorps Corps { get; }
}