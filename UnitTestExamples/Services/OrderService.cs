using UnitTestExamples.Dtos;
using UnitTestExamples.Interfaces;
using UnitTestExamples.Models;

namespace UnitTestExamples.Services;

public class OrderService : IOrderService
{
    private const decimal vatFactor1 = 1.25m;
    private const decimal vatFactor2 = 1.12m;
    private readonly IDatabaseRepository _databaseRepository;
    private readonly IProductService _productService;

    public OrderService(IDatabaseRepository databaseRepository, IProductService productService)
    {
        _databaseRepository = databaseRepository;
        _productService = productService;
    }

    public Order CreateOrder(int customerId, OrderDto order)
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
        catch (Exception)
        {
            // Should probably log the exception here.
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

    private OrderLinePrices CalcPrices(int productId, int quantity)
    {
        var product = _productService.GetProduct(productId);

        var totalNetPrice = product.NetPrice * quantity;

        decimal totalPriceWithVat;

        // TODO: For edgecase testing.
        if (totalNetPrice > 1000.00m)
        {
            totalPriceWithVat = totalNetPrice * vatFactor1;
        } else
        {
            totalPriceWithVat = totalNetPrice * vatFactor2;
        }

        return new OrderLinePrices
        {
            TotalNetPrice = totalNetPrice,
            TotalPriceWithVat = totalPriceWithVat
        };
    }
}
