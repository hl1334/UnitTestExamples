namespace UnitTestExamples.Interfaces;

public interface ICalculate
{
    decimal CalcAmountWithVat(decimal amount, decimal vatFactor);

    int CalcCubicValue(int x, int y, int z);
}
