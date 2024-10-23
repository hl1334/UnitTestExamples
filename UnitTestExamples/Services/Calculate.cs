using UnitTestExamples.Interfaces;

namespace UnitTestExamples.Services;

public class Calculate : ICalculate
{
    public decimal CalcAmountWithVat(decimal amount, decimal vatFactor)
    {
        return amount * vatFactor;
    }

    public int CalcCubicValue(int x, int y, int z)
    {
        return x * y * z;
    }
}
