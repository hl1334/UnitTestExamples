using Microsoft.Extensions.Logging;
using NSubstitute;
using UnitTestExamples.Interfaces;
using UnitTestExamples.Models;
using UnitTestExamples.Services;

namespace UnitTestExamples.Tests;

public class CustomerServiceTests
{
    private CustomerService _uut;

    [Fact]
    public void GetCustomerReturnsValidCustomerData()
    {
        // Arrange
        var repository = Substitute.For<IDatabaseRepository>();
        repository.GetCustomer(Arg.Any<int>()).Returns(x => new Customer
        {
            Id = 1,
            Name = "Hans Testesen",
            AddressId = 1
        });
        repository.GetAddress(Arg.Is(1)).Returns(x => new Address 
        { 
            Id = 1,
            Street = "Test Road 1",
            PostalCode = "T1234",
            City = "Test City",
            Country = "DK",
            Region = "Jutland",
            Email = "test@test.com",
            Phone = "11223344"
        });

        var logger = Substitute.For<ILogger<CustomerService>>();

        var customerMapper = new CustomerMapper();

        _uut = new CustomerService(repository, customerMapper, logger);

        // Act
        var result = _uut.GetCustomer(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Hans Testesen", result.Name);
        Assert.Equal("Test Road 1", result.Street);
        Assert.Equal("T1234", result.PostalCode);
    }

    [Fact]
    public void GetCustomerThrowsExceptionIfNullReturnedFromRepositoryGetCustomer()
    {
        // Arrange
        var repository = Substitute.For<IDatabaseRepository>();
        repository.GetCustomer(Arg.Any<int>()).Returns(x => null);
        repository.GetAddress(Arg.Is(1)).Returns(x => new Address
        {
            Id = 1,
            Street = "Test Road 1",
            PostalCode = "T1234",
            City = "Test City",
            Country = "DK",
            Region = "Jutland",
            Email = "test@test.com",
            Phone = "11223344"
        });

        var logger = Substitute.For<ILogger<CustomerService>>();

        var customerMapper = new CustomerMapper();

        _uut = new CustomerService(repository, customerMapper, logger);

        // Assert
        var exceptionThrown = Assert.Throws<Exception>(() => _uut.GetCustomer(1));
        Assert.Equal("Customer cannot be found", exceptionThrown.Message);
    }

    [Fact]
    public void GetCustomerThrowsExceptionIfNullReturnedFromRepositoryGetAddress()
    {
        // Arrange
        var repository = Substitute.For<IDatabaseRepository>();
        repository.GetCustomer(Arg.Any<int>()).Returns(x => new Customer
        {
            Id = 1,
            Name = "Hans Testesen",
            AddressId = 1
        });
        repository.GetAddress(Arg.Is(1)).Returns(x => null);

        var logger = Substitute.For<ILogger<CustomerService>>();

        var customerMapper = new CustomerMapper();

        _uut = new CustomerService(repository, customerMapper, logger);

        // Assert
        var exceptionThrown = Assert.Throws<Exception>(() => _uut.GetCustomer(1));
        Assert.Equal("Address cannot be found", exceptionThrown.Message);
    }

    [Fact]
    public void GetCustomerRethrowsExceptionIfRepositoryThrowsException()
    {
        // Arrange
        var repository = Substitute.For<IDatabaseRepository>();
        repository.GetCustomer(Arg.Any<int>()).Returns(x => throw new Exception("Some error occured in Repository"));

        var logger = Substitute.For<ILogger<CustomerService>>();

        var customerMapper = new CustomerMapper();

        _uut = new CustomerService(repository, customerMapper, logger);

        // Act and Assert
        var exceptionThrown = Assert.Throws<Exception>(() => _uut.GetCustomer(1));
        Assert.Equal("Some error occured in Repository", exceptionThrown.Message);
    }

    [Fact]
    public void DeleteCustomerLogsErrorOnceWhenExceptionIsThrown()
    {
        // Arrange
        var repository = Substitute.For<IDatabaseRepository>();
        repository.When(x => x.DeleteCustomer(Arg.Any<int>())).Do(x => throw new Exception("Some error occured in Repository"));

        var logger = Substitute.For<ILogger<CustomerService>>();

        var customerMapper = new CustomerMapper();

        _uut = new CustomerService(repository, customerMapper, logger);

        // Act and Assert
        Assert.Throws<Exception>(() => _uut.DeleteCustomer(1));
        logger.Received(1).LogError("Some error occured in Repository");
    }
}
