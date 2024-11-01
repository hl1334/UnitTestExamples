using UnitTestExamples.Models;

namespace UnitTestExamples.Interfaces;

public interface IProductService
{
    Product GetProduct(int productId);
}
