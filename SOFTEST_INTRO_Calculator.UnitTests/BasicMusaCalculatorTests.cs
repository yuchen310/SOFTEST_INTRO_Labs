using SOFTEST_INTRO_Calculator;

namespace SOFTEST_INTRO_Calculator.UnitTests;

[TestFixture]
public class BasicMusaCalculatorTests
{
    private Calculator _calc = new Calculator();

    [Test]
    public void FailureIntensity_ComputesExpectedValue()
    {
        var result = _calc.FailureIntensity(10, 100, 5);
        Assert.That(result, Is.EqualTo(9.51229).Within(0.0001));
    }

    [TestCase(0, 100, 5)]
    [TestCase(-1, 100, 5)]
    [TestCase(10, 0, 5)]
    [TestCase(10, 100, -1)]
    public void FailureIntensity_RejectsInvalidInputs(double lambda0, double nu0, double t)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calc.FailureIntensity(lambda0, nu0, t));
    }

    [Test]
    public void CumulativeFailures_ComputesExpectedValue()
    {
        var result = _calc.CumulativeFailures(10, 100, 5);
        Assert.That(result, Is.EqualTo(4.87706).Within(0.0001));
    }

    [TestCase(0, 100, 5)]
    [TestCase(10, 0, 5)]
    [TestCase(10, 100, -1)]
    public void CumulativeFailures_RejectsInvalidInputs(double lambda0, double nu0, double t)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calc.CumulativeFailures(lambda0, nu0, t));
    }
}