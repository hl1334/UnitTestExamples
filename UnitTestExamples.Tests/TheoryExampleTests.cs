using UnitTestExamples.Services;

namespace UnitTestExamples.Tests;

public class TheoryExampleTests
{
    private readonly Calculate _uut = new();

    public static TheoryData<decimal, decimal, decimal> AmountWithVatTestData =>
        new()
        {
            { 100.00m, 1.25m, 125.00m },
            { 125.00m, 1.25m, 156.25m },
            { 150.00m, 1.25m, 187.50m }
        };

    [Theory]
    [MemberData(nameof(AmountWithVatTestData))]
    public void CalculateAmountWithVatReturnsCorrectAmounts(decimal amount, decimal vatFactor, decimal expected)
    {
        var result = _uut.CalcAmountWithVat(amount, vatFactor);

        Assert.NotEqual(0, result);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10, 20, 30, 6000)]
    [InlineData(5, 10, 15, 750)]
    [InlineData(-2, -3, 4, 24)]
    public void CalculateCubicValueReturnsCorrectValues(int x, int y, int z, decimal expected)
    {
        var result = _uut.CalcCubicValue(x, y, z);

        Assert.NotEqual(0, result);
        Assert.Equal(expected, result);
    }
}
