using UnitTestExamples.Dtos;
using UnitTestExamples.Models;

namespace UnitTestExamples.Interfaces
{
    public interface ICustomerMapper
    {
        CustomerDto Map(Customer customer, Address address);
    }
}
