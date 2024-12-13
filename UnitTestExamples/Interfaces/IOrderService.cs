using UnitTestExamples.Dtos;
using UnitTestExamples.Models;

namespace UnitTestExamples.Interfaces;

public interface IOrderService
{
    Order CreateOrder(OrderDto order);
    OrderLinePrices CalcPrices(int productId, int quantity);
}
