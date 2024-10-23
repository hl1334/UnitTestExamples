using UnitTestExamples.Dtos;
using UnitTestExamples.Interfaces;
using UnitTestExamples.Models;

namespace UnitTestExamples.Services;

public class OrderService : IOrderService
{
    private readonly IDatabaseRepository _databaseRepository;

    public OrderService(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository;
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

    private static List<OrderLine> MapOrderLines(List<OrderLineDto> orderLines)
    {
        return orderLines.Select(line => new OrderLine
        {
            ProductId = line.ProductId,
            Quantity = line.Quantity
        }).ToList();
    }
}
