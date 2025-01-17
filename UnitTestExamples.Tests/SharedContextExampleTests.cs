using UnitTestExamples.Models;
using UnitTestExamples.Services;

namespace UnitTestExamples.Tests;

public class SharedContextExampleTests
{
    private CustomerMapper _uut = new();
    private Customer _customer;
    private Address _address;

    public SharedContextExampleTests()
    {
        _customer = new()
        {
            Name = "Hans Testsen",
            Id = 1,
            AddressId = 1
        };
        _address = new()
        {
            Id = 1,
            Street = "Test Road 1",
            City = "Test City",
            PostalCode = "Z1234",
            Country = "DK",
            Email = "test@testmail.com",
            Phone = "12233445",
            Region = "Jutland"
        };
    }

    [Fact]
    public void CustomerMapperReturnsCorrectCustomerSpecificData()
    {
        // Act
        var result = _uut.Map(_customer, _address);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Hans Testsen", result.Name);
    }

    [Fact]
    public void CustomerMapperReturnsCorrectAddressSpecificData()
    {
        // Act
        var result = _uut.Map(_customer, _address);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Road 1", result.Street);
    }

    [Fact]
    public void CustomerMapperReturnsCorrectData()
    {
        // Act
        var result = _uut.Map(_customer, _address);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Hans Testsen", result.Name);
        Assert.Equal("Test Road 1", result.Street);
    }
}
