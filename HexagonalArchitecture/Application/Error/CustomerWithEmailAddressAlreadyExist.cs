namespace HexagonalArchitecture.Application.Error
{
    public class CustomerWithEmailAddressAlreadyExist : Exception
    {
        public string EmailAddress { get; }

        public CustomerWithEmailAddressAlreadyExist(string message, string emailAddress)
            : base(message)
        {
            EmailAddress = emailAddress;
        }

        public CustomerWithEmailAddressAlreadyExist(string message, string emailAddress, Exception inner)
            : base(message, inner)
        {
            EmailAddress = emailAddress;
        }
    }
}