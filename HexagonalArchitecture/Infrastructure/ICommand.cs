namespace HexagonalArchitecture.Infrastructure;

public interface ICommand
{
    Guid Id { get; }

    static Guid GenerateId()
    {
        return Guid.NewGuid();
    }
}