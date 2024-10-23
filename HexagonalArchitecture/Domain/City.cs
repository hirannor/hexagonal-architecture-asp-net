namespace HexagonalArchitecture.Domain;

public class City
{
    public string Name { get; }

    private City(string name)
    {
        Name = name;
    }

    public static City From(string name)
    {
        return new City(name);
    }
}