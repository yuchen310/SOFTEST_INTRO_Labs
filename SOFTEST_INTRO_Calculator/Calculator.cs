namespace SOFTEST_INTRO_Calculator;

// This class holds ONLY arithmetic/business logic - no Console.ReadLine/WriteLine.
// Keeping I/O out of here is what makes it easy to unit test (no user input needed
// to run a test, and no console dependency to mock).
public class Calculator
{
   // KEYWORDS: addition — returns the sum of two numbers.
    public double Add(double a, double b) => a + b;

    // KEYWORDS: subtraction — returns a minus b.
    public double Subtract(double a, double b) => a - b;

     // KEYWORDS: multiplication — returns the product of two numbers.
    public double Multiply(double a, double b) => a * b;

    // KEYWORDS: division, zero-divisor rule — returns a / b, but throws
    // ArgumentException when the divisor (b) is zero, including 0 / 0.
    // A zero numerator with a nonzero divisor is valid and returns 0.
    public double Divide(double a, double b)
    {
        if (b == 0)
            throw new ArgumentException("Cannot divide by zero.", nameof(b));

        return a / b;
    }

    // --- DoOperation ----------------------------------------------------------
    // Dispatches to the right operation based on a single-character op code.
    // A switch EXPRESSION (not switch statement) - each arm returns a value.
    // The "_" arm is the default/catch-all case: anything not "a"/"s"/"m"/"d"
    // throws. Good candidate for a test that passes an invalid op string.

     // ArgumentException for any unrecognised code.
    public double DoOperation(double a, double b, string op)
    {
        return op switch
        {
            "a" => Add(a, b),
            "s" => Subtract(a, b),
            "m" => Multiply(a, b),
            "d" => Divide(a, b),
            _ => throw new ArgumentException("Unknown operation.")
        };
    }

    // --- Factorial (TDD exercise, section 6) --------------------------------
    // Valid domain: 0 <= n <= 20 (20! is the largest factorial that fits in a
    // signed 64-bit long without overflowing - 21! would overflow, hence the cap);
    // throws ArgumentOutOfRangeException outside that range
    public long Factorial(int n)
    {
        if (n < 0 || n > 20)
            throw new ArgumentOutOfRangeException(nameof(n), "n must be between 0 and 20.");

        long result = 1L;
        for (int i = 2; i <= n; i++)   // starts at 2 since multiplying by 1 is a no-op
            result *= i;

        return result;
    }

    // --- TriangleArea (geometry exercise) -----------------------------------
    // area = 1/2 * base * height. Zero height/width is a valid edge case (area 0).
    // Negative dimensions are treated as invalid input -> throw
   public double TriangleArea(double height, double width)
    {
        if (height < 0)
            throw new ArgumentOutOfRangeException(nameof(height), "Height cannot be negative.");
        if (width < 0)
            throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be negative.");

        return 0.5 * height * width;
    }

    // --- CircleArea (geometry exercise) --------------------------------------
    // area = pi * r^2. Because Math.PI is irrational/floating point, tests
    // comparing against it should use a tolerance (Is.EqualTo(x).Within(...))
    // rather than exact equality.
    // Negative radius are treated as invalid input -> throw
    public double CircleArea(double radius)
    {
        if (radius < 0)
            throw new ArgumentOutOfRangeException(nameof(radius), "Radius cannot be negative.");

        return Math.PI * radius * radius;
    }

    // --- UnknownFunctionA (section 7 extension) -----------------------------
    // From the worked examples in the handout (e.g. n=5,r=3 -> 60; n=5,r=0 -> 1),
    // this matches the "permutations" formula: nPr = n! / (n - r)!
    // Guard clause enforces 0 <= r <= n <= 20 (reuses the same domain as Factorial,
    // since it calls Factorial internally).
    public long UnknownFunctionA(int n, int r)
    {
        if (n < 0 || n > 20 || r < 0 || r > n)
            throw new ArgumentOutOfRangeException(nameof(r), "r must satisfy 0 <= r <= n <= 20.");

        return Factorial(n) / Factorial(n - r);
    }

    // --- UnknownFunctionB (section 7 extension) -----------------------------
    // From the examples (e.g. n=5,r=3 -> 10; n=5,r=0 -> 1), this matches the
    // "combinations" formula: nCr = n! / (r! * (n - r)!)
    // Note UnknownFunctionA(n,r) == UnknownFunctionB(n,r) * Factorial(r) - a nice
    // relationship you could turn into an extra cross-check test.
    public long UnknownFunctionB(int n, int r)
    {
        if (n < 0 || n > 20 || r < 0 || r > n)
            throw new ArgumentOutOfRangeException(nameof(r), "r must satisfy 0 <= r <= n <= 20.");

        return Factorial(n) / (Factorial(r) * Factorial(n - r));
    }

    // --- MTBF (reliability exercise) ----------------------------------------
    // MTBF = operating time / failure count. Both operating time and failure
    // count must be strictly positive; the result is an observed average, not
    // a prediction of the next failure's exact timing.
    public double Mtbf(double operatingTime, double failureCount)
    {
        if (operatingTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(operatingTime), "Operating time must be positive.");
        if (failureCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(failureCount), "Failure count must be positive.");

        return operatingTime / failureCount;
    }

    // --- Availability (reliability exercise) --------------------------------
    // Availability = MTBF / (MTBF + MTTR), using the common repairable-system
    // approximation MTTR = MTBF. Neither value may be negative, and their sum
    // must be positive. Returns a ratio from 0 to 1.
    public double Availability(double mtbf, double mttr)
    {
        if (mtbf < 0)
            throw new ArgumentOutOfRangeException(nameof(mtbf), "MTBF cannot be negative.");
        if (mttr < 0)
            throw new ArgumentOutOfRangeException(nameof(mttr), "MTTR cannot be negative.");
        if (mtbf + mttr <= 0)
            throw new ArgumentOutOfRangeException(nameof(mtbf), "MTBF + MTTR must be positive.");

        return mtbf / (mtbf + mttr);
    }

    // --- Basic Musa reliability-growth model (Lecture 1) ---------------------
    // lambda0: initial failure intensity, nu0: expected total failures for
    // infinite execution time, t: accumulated execution time.
    // Domain: lambda0 > 0, nu0 > 0, t >= 0.
    public double FailureIntensity(double lambda0, double nu0, double t)
{
    ValidateMusaInputs(lambda0, nu0, t);
    return lambda0 * Math.Exp(-t / nu0);
}

public double CumulativeFailures(double lambda0, double nu0, double t)
{
    ValidateMusaInputs(lambda0, nu0, t);
    return nu0 * (1 - Math.Exp(-t / nu0));
}
    private static void ValidateMusaInputs(double lambda0, double nu0, double t)
    {
        if (lambda0 <= 0)
            throw new ArgumentOutOfRangeException(nameof(lambda0), "Initial failure intensity must be positive.");
        if (nu0 <= 0)
            throw new ArgumentOutOfRangeException(nameof(nu0), "Expected total failures must be positive.");
        if (t < 0)
            throw new ArgumentOutOfRangeException(nameof(t), "Execution time cannot be negative.");
    }

public double GenMagicNum(
    int choice, string path, IFileReader fileReader)
{
    ArgumentNullException.ThrowIfNull(fileReader);

    if (choice < 0)
    {
        throw new ArgumentOutOfRangeException(nameof(choice));
    }

    string[] magicStrings = fileReader.Read(path);

    if (choice >= magicStrings.Length)
    {
        throw new ArgumentOutOfRangeException(nameof(choice));
    }

    double magicNumber = double.Parse(magicStrings[choice]);
    return 2 * Math.Abs(magicNumber);
}
}