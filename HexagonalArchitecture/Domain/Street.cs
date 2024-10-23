namespace HexagonalArchitecture.Domain;

public class Street
{
    public string Name { get; }
    public string Number { get; }

    private Street(string name, string number)
    {
        Name = name;
        Number = number;
    }

    public static Street From(string name, string number)
    {
        return new Street(name, number);
    }
}