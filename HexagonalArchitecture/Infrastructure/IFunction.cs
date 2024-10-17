namespace HexagonalArchitecture.Infrastructure;

public interface IFunction<in T, out TD>
{
    public TD Apply(T input);
}