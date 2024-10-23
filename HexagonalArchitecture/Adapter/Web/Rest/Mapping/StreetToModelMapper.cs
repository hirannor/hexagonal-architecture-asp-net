using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Web.Rest.Mapping;

public class StreetToModelMapper : IFunction<Street, StreetModel>
{
    public StreetModel Apply(Street? input)
    {
        if (input is null) return null;

        return StreetModel.From(
            input.Name,
            input.Number
        );
    }
}