using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework.Mapping;

public static class CustomerMappingFactory
{
    public static IFunction<Customer, CustomerModel> UserToModelMapper()
    {
        return new CustomerToModelMapper();
    }

    public static IFunction<CustomerModel, Customer> UserModelToDomainMapper()
    {
        return new CustomerModelToDomainMapper();
    }
}