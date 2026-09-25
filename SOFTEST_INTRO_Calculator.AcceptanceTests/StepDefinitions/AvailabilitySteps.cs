using NUnit.Framework; 
using Reqnroll; 
using SOFTEST_INTRO_Calculator; 
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support; 


[Binding]
public class AvailabilitySteps
{
    private readonly CalculatorContext _calculator;
    private readonly ReliabilityContext _reliability;

    public AvailabilitySteps(CalculatorContext calculator, ReliabilityContext reliability)
    {
        _calculator = calculator;
        _reliability = reliability;
    }

    [Given("the reliability values are")]
    public void GivenTheReliabilityValuesAre(DataTable table)
    {
        var values = table.Rows[0];
        _reliability.Mtbf = double.Parse(values["MTBF"]);
        _reliability.Mttr = double.Parse(values["MTTR"]);
    }

[When("I have entered {double} and {double} into the calculator and press Availability")]
public void WhenIHaveEnteredIntoTheCalculatorAndPressAvailability(double a, double b)
{
    try
    {
        _calculator.Result = _calculator.Calculator.Availability(a, b);
    }
    catch (Exception ex)
    {
        _calculator.Error = ex;
    }
}
}