@Addition 
Feature: UsingCalculatorAddition 
  In order to avoid mistakes 
  As a calculator user 
  I want to be told the sum of two numbers 
 
Scenario Outline: Add zeros for special cases 
    Given I have a calculator 
    When I have entered <value1> and <value2> into the calculator and press add 
    Then the result should be <value3> 
 
    Examples: 
      | value1 | value2 | value3 | 
      | 1      | 11     | 12      | 
      | 10     | 11     | 21     | 
      | 11     | 11     | 22     |