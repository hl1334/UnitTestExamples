using UnitTestExamples.Dtos;
using UnitTestExamples.Interfaces;
using UnitTestExamples.Models;

namespace UnitTestExamples.Services;

public class CustomerMapper : ICustomerMapper
{
    public CustomerDto Map(Customer customer, Address address)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentNullException.ThrowIfNull(address);

        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Street = address.Street,
            City = address.City,
            Region = address.Region,
            PostalCode = address.PostalCode,
            Country = address.Country,
            Email = address.Email,
            Phone = address.Phone,
        };
    }
}
