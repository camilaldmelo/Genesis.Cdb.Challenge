using FluentAssertions;
using Genesis.Cdb.Challenge.Domain.Services;

namespace Genesis.Cdb.Challenge.UnitTests.Domain.Services;

public class CdbCalculatorTests
{
    private readonly CdbCalculator _calculator;

    public CdbCalculatorTests()
    {
        _calculator = new CdbCalculator();
    }

    [Fact]
    public void Should_Calculate_Gross_And_Net_Amounts()
    {
        // Arrange
        decimal initialAmount = 1000m;
        int months = 12;

        // Act
        var result = _calculator.Calculate(
            initialAmount,
            months);

        // Assert
        result.Should().NotBeNull();

        result.GrossAmount.Should()
            .BeGreaterThan(initialAmount);

        result.NetAmount.Should()
            .BeLessThan(result.GrossAmount);

        result.NetAmount.Should()
            .BeGreaterThan(initialAmount);
    }

    [Theory]
    [InlineData(2, 0.225)]
    [InlineData(6, 0.225)]
    [InlineData(7, 0.20)]
    [InlineData(12, 0.20)]
    [InlineData(13, 0.175)]
    [InlineData(24, 0.175)]
    [InlineData(25, 0.15)]
    public void Should_Apply_Correct_Tax_For_All_Ranges(
        int months,
        decimal expectedTax)
    {
        // Arrange
        decimal initialAmount = 1000m;

        // Act
        var result = _calculator.Calculate(
            initialAmount,
            months);

        decimal grossProfit =
            result.GrossAmount - initialAmount;

        decimal netProfit =
            result.NetAmount - initialAmount;

        decimal taxApplied =
            1 - netProfit / grossProfit;

        // Assert
        Math.Round(taxApplied, 3)
            .Should()
            .Be(Math.Round(expectedTax, 3));
    }

    [Fact]
    public void Should_Return_Values_Rounded_To_Two_Decimal_Places()
    {
        // Arrange
        decimal initialAmount = 1234.56m;
        int months = 7;

        // Act
        var result = _calculator.Calculate(
            initialAmount,
            months);

        // Assert
        decimal.Round(result.GrossAmount, 2)
            .Should()
            .Be(result.GrossAmount);

        decimal.Round(result.NetAmount, 2)
            .Should()
            .Be(result.NetAmount);
    }

    [Fact]
    public void Net_Amount_Should_Be_Greater_Than_Initial_Amount()
    {
        // Arrange
        decimal initialAmount = 5000m;
        int months = 10;

        // Act
        var result = _calculator.Calculate(
            initialAmount,
            months);

        // Assert
        result.NetAmount
            .Should()
            .BeGreaterThan(initialAmount);
    }

    [Fact]
    public void Should_Calculate_Compound_Interest_Using_Monthly_Formula()
    {
        // Arrange
        decimal initialAmount = 1000m;
        int months = 2;

        decimal expectedGrossAmount =
            initialAmount
            * (1 + (0.009m * 1.08m))
            * (1 + (0.009m * 1.08m));

        // Act
        var result = _calculator.Calculate(
            initialAmount,
            months);

        // Assert
        result.GrossAmount
            .Should()
            .Be(Math.Round(expectedGrossAmount, 2));
    }

    [Fact]
    public void Should_Calculate_Reference_GrossAmount_For_6_Months()
    {
        // Act
        var result = _calculator.Calculate(
            1000m,
            6);

        // Assert
        result.GrossAmount
            .Should()
            .Be(1059.76m);
    }

    [Fact]
    public void Should_Calculate_Reference_GrossAmount_For_12_Months()
    {
        // Act
        var result = _calculator.Calculate(
            1000m,
            12);

        // Assert
        result.GrossAmount
            .Should()
            .Be(1123.08m);
    }
}