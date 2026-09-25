using NUnit.Framework; 
using Reqnroll; 
using SOFTEST_INTRO_Calculator; 
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support; 
 
namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions; 
 
[Binding] 
public sealed class CommonCalculatorSteps 
{ 
    private readonly CalculatorContext _context; 
 
    public CommonCalculatorSteps(CalculatorContext context) 
    { 
        _context = context; 
    } 
 
    [Given("I have a calculator")] 
    public void GivenIHaveACalculator()
     { 
        _context.Calculator = new Calculator(); 
        _context.Result = null; 
        _context.IntegerResult = null; 
        _context.Error = null; 
    } 
 
    [Then("the result should be {double}")] 
    public void ThenTheResultShouldBe(double expected) 
    { 
        Assert.That(_context.Result, 
            Is.EqualTo(expected).Within(1e-9)); 
    } 
 
    [Then("division should be rejected")] 
    public void ThenDivisionShouldBeRejected() 
    { 
        Assert.That(_context.Error,  
        Is.TypeOf<ArgumentException>()); 
    } 

    [Then("the factorial result should be {long}")]
    public void ThenTheFactorialResultShouldBe(long expected)
    {
        Assert.That(_context.IntegerResult, Is.EqualTo(expected));
    }
} 