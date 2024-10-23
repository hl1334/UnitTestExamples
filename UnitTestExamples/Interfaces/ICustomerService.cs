using UnitTestExamples.Dtos;

namespace UnitTestExamples.Interfaces;

public interface ICustomerService
{
    CustomerDto GetCustomer(int customerId);
    void DeleteCustomer(int customerId);
}
