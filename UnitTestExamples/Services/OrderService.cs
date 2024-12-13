using Microsoft.Extensions.Logging;
using UnitTestExamples.Dtos;
using UnitTestExamples.Interfaces;
using UnitTestExamples.Models;

namespace UnitTestExamples.Services;

public class OrderService : IOrderService
{
    private const decimal VatFactor1 = 1.25m;
    private const decimal VatFactor2 = 1.12m;
    private const decimal NetPriceThreshold = 1000.00m;

    private readonly IDatabaseRepository _databaseRepository;
    private readonly IProductService _productService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IDatabaseRepository databaseRepository, IProductService productService, ILogger<OrderService> logger)
    {
        _databaseRepository = databaseRepository;
        _productService = productService;
        _logger = logger;
    }

    public Order CreateOrder(OrderDto order)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(order);
            ArgumentNullException.ThrowIfNull(order.OrderLines);

            var newOrder = new Order
            {
                CustomerId = order.CustomerId,
                Requisition = order.CustomerMessage,
                OrderLines = MapOrderLines(order.OrderLines)
            };

            var createdOrder = _databaseRepository.UpsertOrder(newOrder);

            return createdOrder;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    private List<OrderLine> MapOrderLines(List<OrderLineDto> orderLines)
    {
        return orderLines.Select(line => new OrderLine
        {
            ProductId = line.ProductId,
            Quantity = line.Quantity,
            Prices = CalcPrices(line.ProductId, line.Quantity)
        }).ToList();
    }

    public OrderLinePrices CalcPrices(int productId, int quantity)
    {
        var product = _productService.GetProduct(productId);

        var totalNetPrice = product.NetPrice * quantity;

        decimal totalPriceWithVat;

        // TODO: For edgecase testing.
        if (totalNetPrice > NetPriceThreshold)
        {
            totalPriceWithVat = totalNetPrice * VatFactor1;
        } else
        {
            totalPriceWithVat = totalNetPrice * VatFactor2;
        }

        return new OrderLinePrices
        {
            TotalNetPrice = totalNetPrice,
            TotalPriceWithVat = totalPriceWithVat
        };
    }
}
