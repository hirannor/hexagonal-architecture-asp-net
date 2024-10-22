namespace HexagonalArchitecture.Application.Error;

public class CustomerNotFound : Exception
{
    public CustomerNotFound(string message) : base(message)
    {
    }

    public CustomerNotFound(string message, Exception inner) : base(message)
    {
    }
}