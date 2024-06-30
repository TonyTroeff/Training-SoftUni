namespace MilitaryElite.Interfaces;

using MilitaryElite.Enums;

public interface IMission
{
    string CodeName { get; }
    MissionState State { get; }
}