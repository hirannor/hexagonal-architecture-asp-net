using HexagonalArchitecture.Domain;

namespace HexagonalArchitecture.Application.Error
{
    public class CustomerWithEmailAddressAlreadyExist : Exception
    {
        public EmailAddress EmailAddress { get; }

        public CustomerWithEmailAddressAlreadyExist(string message, EmailAddress emailAddress)
            : base(message)
        {
            EmailAddress = emailAddress;
        }

        public CustomerWithEmailAddressAlreadyExist(string message, EmailAddress emailAddress, Exception inner)
            : base(message, inner)
        {
            EmailAddress = emailAddress;
        }
    }
}