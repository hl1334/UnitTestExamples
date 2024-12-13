namespace UnitTestExamples.Models;

public class Product
{
    public int Id { get; set; }
    public required string Sku { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal NetPrice { get; set; }
}
