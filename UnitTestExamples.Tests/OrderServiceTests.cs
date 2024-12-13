using Microsoft.Extensions.Logging;
using NSubstitute;
using UnitTestExamples.Interfaces;
using UnitTestExamples.Models;
using UnitTestExamples.Services;

namespace UnitTestExamples.Tests;

public class OrderServiceTests
{
    private OrderService _uut;
    private readonly IDatabaseRepository _repository;
    private readonly IProductService _productService;
    private readonly ILogger<OrderService> _logger;

    public OrderServiceTests()
    {
        _repository = Substitute.For<IDatabaseRepository>();
        _productService = Substitute.For<IProductService>();
        _logger = Substitute.For<ILogger<OrderService>>();
        _productService.GetProduct(1).Returns(new Product
        {
            Sku = "12345",
            Name = "Test Product 1",
            Description = "Description for Test Product 2",
            Id = 1,
            NetPrice = 100.00m
        });
        _productService.GetProduct(2).Returns(new Product
        {
            Sku = "54321",
            Name = "Test Product 2",
            Description = "Description for Test Product 2",
            Id = 2,
            NetPrice = 100.01m
        });
    }

    // For edge case testing
    [Fact]
    public void CalcPricesWhenNetPriceBelow1000ReturnsValidPrices()
    {
        // Arrange
        _uut = new OrderService(_repository, _productService, _logger);

        // Act
        var result = _uut.CalcPrices(1, 10);

        // Assert
        Assert.Equal(1000m, result.TotalNetPrice);
        Assert.Equal(1120m, result.TotalPriceWithVat);
    }

    // For edge case testing
    [Fact]
    public void CalcPricesWhenNetPriceAbove1000ReturnsValidPrices()
    {
        // Arrange
        _uut = new OrderService(_repository, _productService, _logger);

        // Act
        var result = _uut.CalcPrices(2, 10);

        // Assert
        Assert.Equal(1000.10m, result.TotalNetPrice);
        Assert.Equal(1250.125m, result.TotalPriceWithVat);
    }
}
