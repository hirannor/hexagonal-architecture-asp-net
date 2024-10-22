namespace HexagonalArchitecture.Application.Error;

public class EmailAddressAlreadyExist : Exception
{
    public EmailAddressAlreadyExist(string message)
        : base(message)
    {
    }

    public EmailAddressAlreadyExist(string message, Exception inner)
        : base(message, inner)
    {
    }
}