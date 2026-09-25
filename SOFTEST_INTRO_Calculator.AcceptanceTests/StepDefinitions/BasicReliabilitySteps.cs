using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class BasicReliabilitySteps
{
    private readonly CalculatorContext _context;
    private readonly BasicReliabilityContext _reliability;

    public BasicReliabilitySteps(CalculatorContext context, BasicReliabilityContext reliability)
    {
        _context = context;
        _reliability = reliability;
    }

    [Given("the initial failure intensity is {double}")]
    public void GivenTheInitialFailureIntensityIs(double lambda0)
    {
        _reliability.Lambda0 = lambda0;
    }

    [Given("the expected total number of failures is {double}")]
    public void GivenTheExpectedTotalNumberOfFailuresIs(double nu0)
    {
        _reliability.Nu0 = nu0;
    }

    [When("I calculate the failure intensity at time {double}")]
    public void WhenICalculateTheFailureIntensityAtTime(double t)
    {
        _context.Result = null;
        _context.Error = null;

        try
        {
            _context.Result = _context.Calculator.FailureIntensity(
                _reliability.Lambda0!.Value, _reliability.Nu0!.Value, t);
        }
        catch (ArgumentOutOfRangeException error)
        {
            _context.Error = error;
        }
    }

    [When("I calculate the cumulative failures at time {double}")]
    public void WhenICalculateTheCumulativeFailuresAtTime(double t)
    {
        _context.Result = null;
        _context.Error = null;

        try
        {
            _context.Result = _context.Calculator.CumulativeFailures(
                _reliability.Lambda0!.Value, _reliability.Nu0!.Value, t);
        }
        catch (ArgumentOutOfRangeException error)
        {
            _context.Error = error;
        }
    }

    [Then("the result should be approximately {double}")]
    public void ThenTheResultShouldBeApproximately(double expected)
    {
        Assert.That(_context.Result, Is.EqualTo(expected).Within(0.0001));
    }

    [Then("an error should be reported")]
    public void ThenAnErrorShouldBeReported()
    {
        Assert.That(_context.Error, Is.Not.Null);
        Assert.That(_context.Error, Is.InstanceOf<ArgumentOutOfRangeException>());
    }
}