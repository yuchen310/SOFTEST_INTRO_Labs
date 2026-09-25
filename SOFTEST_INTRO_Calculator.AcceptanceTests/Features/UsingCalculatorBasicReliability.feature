@BasicMusa
Feature: UsingCalculatorBasicReliability
  In order to calculate the Basic Musa model's failures and intensities
  As a Software Quality Reliability enthusiast
  I want to use my calculator to do this

  Scenario: Calculating current failure intensity
    Given I have a calculator
    And the initial failure intensity is 10
    And the expected total number of failures is 100
    When I calculate the failure intensity at time 5
    Then the result should be approximately 9.51229

  Scenario: Calculating expected cumulative failures
    Given I have a calculator
    And the initial failure intensity is 10
    And the expected total number of failures is 100
    When I calculate the cumulative failures at time 5
    Then the result should be approximately 4.87706

  Scenario: Rejecting an invalid initial failure intensity
    Given I have a calculator
    And the initial failure intensity is -10
    And the expected total number of failures is 100
    When I calculate the failure intensity at time 5
    Then an error should be reported

  Scenario: Rejecting a negative execution time
    Given I have a calculator
    And the initial failure intensity is 10
    And the expected total number of failures is 100
    When I calculate the failure intensity at time -1
    Then an error should be reported