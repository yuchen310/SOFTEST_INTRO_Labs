using NUnit.Framework;
using Reqnroll;
using SOFTEST_INTRO_Calculator;
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;
using System;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class FactorialSteps
{
    private readonly CalculatorContext _context;

    public FactorialSteps(CalculatorContext context)
    {
        _context = context;
    }

    [When("I have entered {int} into the calculator and press factorial")]
    public void WhenIHaveEnteredIntoTheCalculatorAndPressFactorial(int value)
    {
        try
        {
            _context.IntegerResult = _context.Calculator.Factorial(value);
        }
        catch (Exception ex)
        {
            _context.Error = ex;
        }
    }
}