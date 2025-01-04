using SimpleSnake.Enums;
using System.Diagnostics.CodeAnalysis;

namespace SimpleSnake.Interfaces;

public interface IReader
{
    bool TryReadDirection([NotNullWhen(true)] out Direction? direction);
    bool ReadConfirmation(char expectedKeyChar);
}
