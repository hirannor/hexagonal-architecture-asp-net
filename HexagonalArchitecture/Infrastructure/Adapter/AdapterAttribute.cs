namespace HexagonalArchitecture.Infrastructure.Adapter;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class AdapterAttribute(AdapterType type) : Attribute
{
    public AdapterType Type { get; set; } = type;
}