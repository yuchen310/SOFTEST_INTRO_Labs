@Factorial
Feature: UsingCalculatorFactorial
  In order to compute factorials safely
  As a calculator user
  I want to know the factorial of a number

  Scenario: Calculate a normal factorial
    Given I have a calculator
    When I have entered 5 into the calculator and press factorial
    Then the factorial result should be 120

  Scenario: Factorial of zero is one
    Given I have a calculator
    When I have entered 0 into the calculator and press factorial
    Then the factorial result should be 1

  Scenario: Reject factorial of a negative number
    Given I have a calculator
    When I have entered -1 into the calculator and press factorial
    Then factorial should be rejected