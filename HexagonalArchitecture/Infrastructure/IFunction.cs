namespace HexagonalArchitecture.Infrastructure;

public interface IFunction<in T, out TD>
{
    TD Apply(T input);
}