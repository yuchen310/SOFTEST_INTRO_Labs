using Reqnroll;
using SOFTEST_INTRO_Calculator;
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.Steps;

[Binding]
public class ReliabilityCalculationSteps
{
    private readonly CalculatorContext _calculator;

    public ReliabilityCalculationSteps(CalculatorContext calculator)
    {
        _calculator = calculator;
    }

    [When("I have entered {double} and {double} into the calculator and press MTBF")]
    public void WhenIHaveEnteredIntoTheCalculatorAndPressMtbf(double a, double b)
    {
        try
        {
            _calculator.Result = _calculator.Calculator.Mtbf(a, b);
        }
        catch (Exception ex)
        {
            _calculator.Error = ex;
        }
    }
}