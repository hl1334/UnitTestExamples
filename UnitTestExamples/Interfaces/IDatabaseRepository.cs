using UnitTestExamples.Models;

namespace UnitTestExamples.Interfaces;

public interface IDatabaseRepository
{
    Customer GetCustomer(int id);
    Customer UpsertCustomer(Customer customer);
    void DeleteCustomer(int id);

    Order GetOrder(int id);
    Order UpsertOrder(Order order);
    void DeleteOrder(int id);

    Product GetProduct(int id);
    Product UpsertProduct(Product product);
    void DeleteProduct(int id);

    Address GetAddress(int id);
    Address UpsertAddress(Address address);
    void DeleteAddress(int id);
}
