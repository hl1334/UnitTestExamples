namespace UnitTestExamples.Dtos;

public class OrderDto
{
    public int CustomerId { get; set; }
    public string? CustomerMessage { get; set; }
    public List<OrderLineDto> OrderLines { get; set; } = [];
}
