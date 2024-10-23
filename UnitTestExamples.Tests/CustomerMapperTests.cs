using UnitTestExamples.Models;
using UnitTestExamples.Services;

namespace UnitTestExamples.Tests;

public class CustomerMapperTests
{
    private CustomerMapper _uut = new();

    [Fact]
    public void CustomerMapperReturnsCorrectCustomerData()
    {
        // Arrange
        Customer customer = new()
        {
            Name = "Hans Testsen",
            Id = 1,
            AddressId = 1
        };
        Address address = new()
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

        // Act
        var result = _uut.Map(customer, address);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Hans Testsen", result.Name);
        Assert.Equal("Test Road 1", result.Street);
    }

    [Fact]
    public void CustomerMapperThrowsArgumentNullExceptionIfCustomerIsNull()
    {
        Customer customer = null;
        Address address = new()
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

        Assert.Throws<ArgumentNullException>(() => _uut.Map(customer, address));
    }

    [Fact]
    public void CustomerMapperThrowsArgumentNullExceptionIfAddressIsNull()
    {
        Customer customer = new()
        {
            Name = "Hans Testsen",
            Id = 1,
            AddressId = 1
        };
        Address address = null;

        Assert.Throws<ArgumentNullException>(() => _uut.Map(customer, address));
    }
}
