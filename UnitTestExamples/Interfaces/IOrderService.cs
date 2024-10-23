using UnitTestExamples.Dtos;
using UnitTestExamples.Models;

namespace UnitTestExamples.Interfaces;

public interface IOrderService
{
    Order CreateOrder(int customerId, OrderDto order);
}
