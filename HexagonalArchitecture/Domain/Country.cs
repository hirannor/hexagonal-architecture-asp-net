namespace HexagonalArchitecture.Domain;

public class Country
{
    public string Name { get; }

    private Country(string name)
    {
        Name = name;
    }

    public static Country From(string name)
    {
        return new Country(name);
    }
}