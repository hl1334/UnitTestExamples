namespace UnitTestExamples.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? Requisition { get; set; }
    public List<OrderLine> OrderLines { get; set; } = [];
}
