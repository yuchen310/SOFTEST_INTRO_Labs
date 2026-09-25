using SOFTEST_INTRO_Calculator;
using NUnit.Framework;

namespace SOFTEST_INTRO_Calculator.UnitTests;

public class CalculatorTests
{
    // Field holding the object under test. "= null!" tells the compiler
    // "trust me, this will be set before it's used" (it is, in SetUp below) -
    // it does NOT create an instance itself.
    private Calculator _calculator = null!;

    // [SetUp] runs before EVERY [Test]/[TestCase] in this class.
    // This guarantees each test gets a *fresh* Calculator, so tests can't
    // accidentally leak state between each other (test independence).
    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
    }

    // --- First test: classic Arrange / Act / Assert (AAA) shape -------------
    // Arrange = set up inputs (done in SetUp here)
    // Act     = call the method under test, capture the result
    // Assert  = check the result against an INDEPENDENTLY chosen expected value
    //           (i.e. don't just re-derive it with the same formula as the code)
    [Test]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        // Arrange: the calculator is created in SetUp.
        // Act
        double result = _calculator.Add(10, 20);
        // Assert
        Assert.That(result, Is.EqualTo(30));
    }

    // --- Parameterised test with [TestCase] ----------------------------------
    // Each [TestCase] attribute runs the SAME method body once per row of data,
    // as a separate, independently-reported test. Cases here cover:
    //   (0,0)      -> both zero
    //   (0,5)      -> zero + positive
    //   (-3,8)     -> negative + positive
    //   (0.1,0.2)  -> floating point rounding case (needs a tolerance, see below)
    // .Within(1e-9) is needed because floating-point arithmetic (e.g. 0.1+0.2)
    // is not exactly representable in binary, so exact equality would fail.
    [TestCase(0, 0, 0)]
    [TestCase(0, 5, 5)]
    [TestCase(-3, 8, 5)]
    [TestCase(0.1, 0.2, 0.3)]
    public void Add_RepresentativeInputs_ReturnsSum(
        double a, double b, double expected)
    {
        double result = _calculator.Add(a, b);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // --- Exception-throwing test ---------------------------------------------
    // Wrapping the call in a lambda "() => ..." DELAYS execution so NUnit can
    // invoke it internally and catch the exception itself - calling
    // _calculator.Divide(a, b) directly would throw immediately and crash the test
    // before the Assert even runs.
    // Covers both a normal zero-divisor case (15/0) and the 0/0 edge case.
    [TestCase(15, 0)]
    [TestCase(0, 0)]
    public void Divide_ZeroDivisor_ThrowsArgumentException(double a, double b)
    {
        Assert.That(() => _calculator.Divide(a, b),
            Throws.TypeOf<ArgumentException>());
    }

    // --- Boundary value: lower edge of Factorial's valid range (0) -----------
    [Test]
    public void Factorial_Zero_ReturnsOne()
    {
        long result = _calculator.Factorial(0);
        Assert.That(result, Is.EqualTo(1L));
    }

    // Covers a small case (1), a mid-size case (5), and the UPPER boundary (20).
    // 20 is included because it's the largest input that doesn't overflow long -
    // this is exactly the kind of "boundary value" the lab asks you to justify.
    [TestCase(1, 1L)]
    [TestCase(5, 120L)]
    [TestCase(20, 2432902008176640000L)]
    public void Factorial_ValidInputs_ReturnsExpected(int n, long expected)
    {
        long result = _calculator.Factorial(n);
        Assert.That(result, Is.EqualTo(expected));
    }

    // Values just OUTSIDE the valid range on both sides: -1 (below 0) and
    // 21 (above 20). Testing "just inside" and "just outside" a boundary
    // together is the standard boundary-value-analysis technique.
    [TestCase(-1)]
    [TestCase(21)]
    public void Factorial_OutOfRange_ThrowsArgumentOutOfRangeException(int n)
    {
        Assert.That(() => _calculator.Factorial(n),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    //Triangle area
    // Covers a normal case (3,4 -> 6) and both "one dimension is zero" edge
    // cases (each should independently give area 0).
    [TestCase(3, 4, 6)]
    [TestCase(0, 5, 0)]
    [TestCase(5, 0, 0)]
    public void TriangleArea_ValidInputs_ReturnsExpected(double height, double width, double expected)
    {
        double result = _calculator.TriangleArea(height, width);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // Covers: negative height only, negative width only, and both negative -
    // all three should throw, confirming each guard clause works independently.
    [TestCase(-1, 4)]
    [TestCase(3, -1)]
    [TestCase(-1, -1)]
    public void TriangleArea_NegativeDimension_ThrowsArgumentOutOfRangeException(double height, double width)
    {
        Assert.That(() => _calculator.TriangleArea(height, width),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    //Circle area
    // radius=1 -> pi is a good "sanity check" case since the multiplier disappears.
    // radius=0 -> edge case, area should be exactly 0.
    // Uses tolerance again because Math.PI is a floating point approximation.
    [TestCase(1, Math.PI)]
    [TestCase(0, 0)]
    public void CircleArea_ValidInputs_ReturnsExpected(double radius, double expected)
    {
        double result = _calculator.CircleArea(radius);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(-1)]
    public void CircleArea_NegativeRadius_ThrowsArgumentOutOfRangeException(double radius)
    {
        Assert.That(() => _calculator.CircleArea(radius),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    //Section 7
    // UnknownFunctionA turned out to be nPr (permutations): n! / (n-r)!
    // Cases (5,5)->120, (5,4)->120, (5,3)->60, (5,0)->1, (0,0)->1 all come
    // straight from the handout's worked example table.
    // The (4,2)->12 case is a "discriminating example": it was ADDED (not in
    // the original table) specifically because it distinguishes nPr (=12)
    // from nCr (=6) for the same inputs - i.e. it rules out the wrong formula.
    [TestCase(5, 5, 120L)]
    [TestCase(5, 4, 120L)]
    [TestCase(5, 3, 60L)]
    [TestCase(5, 0, 1L)]
    [TestCase(0, 0, 1L)]
    [TestCase(4, 2, 12L)]      // discriminating example: not in the original table
    public void UnknownFunctionA_ValidInputs_ReturnsExpected(int n, int r, long expected)
    {
        long result = _calculator.UnknownFunctionA(n, r);
        Assert.That(result, Is.EqualTo(expected));
    }

    // UnknownFunctionB turned out to be nCr (combinations): n! / (r!(n-r)!)
    // Same idea as above - (4,2)->6 discriminates nCr from nPr (which gives 12
    // for the same inputs), proving these two functions really are different.
    [TestCase(5, 5, 1L)]
    [TestCase(5, 4, 5L)]
    [TestCase(5, 3, 10L)]
    [TestCase(5, 0, 1L)]
    [TestCase(0, 0, 1L)]
    [TestCase(4, 2, 6L)]       // discriminating example
    public void UnknownFunctionB_ValidInputs_ReturnsExpected(int n, int r, long expected)
    {
        long result = _calculator.UnknownFunctionB(n, r);
        Assert.That(result, Is.EqualTo(expected));
    }

    // Invalid-input cases for A: n negative, r > n, and r negative respectively -
    // each violates a different part of the "0 <= r <= n <= 20" contract.
    [TestCase(-4, 5)]
    [TestCase(4, 5)]
    [TestCase(5, -1)]
    public void UnknownFunctionA_InvalidInputs_ThrowsArgumentOutOfRangeException(int n, int r)
    {
        Assert.That(() => _calculator.UnknownFunctionA(n, r),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Same invalid-input pattern as above, mirrored for UnknownFunctionB.
    [TestCase(-4, 5)]
    [TestCase(4, 5)]
    [TestCase(5, -1)]
    public void UnknownFunctionB_InvalidInputs_ThrowsArgumentOutOfRangeException(int n, int r)
    {
        Assert.That(() => _calculator.UnknownFunctionB(n, r),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

}