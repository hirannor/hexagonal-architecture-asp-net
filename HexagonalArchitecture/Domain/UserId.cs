namespace HexagonalArchitecture.Domain
{
    public record UserId(string Value)
    {
        public static UserId From(Guid id)
        {
            return new UserId(id.ToString());
        }

        public static UserId From(string id)
        {
            return new UserId(id);
        }

        public static UserId Generate()
        {
            return new UserId(Guid.NewGuid().ToString());
        }
    }
}