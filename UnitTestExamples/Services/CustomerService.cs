using Microsoft.Extensions.Logging;
using UnitTestExamples.Dtos;
using UnitTestExamples.Interfaces;

namespace UnitTestExamples.Services;

public class CustomerService : ICustomerService
{
    private readonly IDatabaseRepository _databaseRepository;
    private readonly ICustomerMapper _customerMapper;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(IDatabaseRepository databaseRepository, ICustomerMapper customerMapper, ILogger<CustomerService> logger)
    {
        _databaseRepository = databaseRepository;
        _customerMapper = customerMapper;
        _logger = logger;
    }

    public void DeleteCustomer(int customerId)
    {
        try
        {
            _databaseRepository.DeleteCustomer(customerId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public CustomerDto GetCustomer(int customerId)
    {
        try
        {
            var customer = _databaseRepository.GetCustomer(customerId) ?? throw new Exception("Customer cannot be found");
            var address = _databaseRepository.GetAddress(customer.AddressId) ?? throw new Exception("Address cannot be found");

            var customerDto = _customerMapper.Map(customer, address);

            return customerDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
