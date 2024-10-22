namespace HexagonalArchitecture.Infrastructure;

public interface IModeller<TO_MUTATE>
{
    TO_MUTATE To(TO_MUTATE model);
}