using UnitTestExamples.Services;

namespace UnitTestExamples.Tests;

public class FactExampleTests
{
    private readonly Calculate _uut = new();

    [Fact]
    public void CalculateAmountWithVatReturnsCorrectAmount()
    {
        // Arrange
        decimal amount = 125.00m;
        decimal vatFactor = 1.25m;

        // Act
        var result = _uut.CalcAmountWithVat(amount, vatFactor);

        // Assert
        Assert.NotEqual(0, result);
        Assert.Equal(156.25m, result);
    }

    [Fact]
    public void CalculateCubicValueReturnsCorrectValue()
    {
        // Arrange
        int x = 10;
        int y = 20;
        int z = 30;

        // Act
        var result = _uut.CalcCubicValue(x, y, z);

        // Assert
        Assert.NotEqual(0, result);
        Assert.Equal(6000, result);
    }
}
