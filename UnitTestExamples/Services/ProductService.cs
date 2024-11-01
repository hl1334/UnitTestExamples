using Microsoft.Extensions.Logging;
using UnitTestExamples.Interfaces;
using UnitTestExamples.Models;

namespace UnitTestExamples.Services;

public class ProductService : IProductService
{
    private readonly IDatabaseRepository _databaseRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IDatabaseRepository databaseRepository, ILogger<ProductService> logger)
    {
        _databaseRepository = databaseRepository;
        _logger = logger;
    }

    public Product GetProduct(int productId)
    {
        try
        {
            var product = _databaseRepository.GetProduct(productId) ?? throw new Exception("Product cannot be found");

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}