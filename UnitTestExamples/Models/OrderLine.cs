namespace UnitTestExamples.Models;

public class OrderLine
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public OrderLinePrices? Prices { get; set; }
}
