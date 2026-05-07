using FluentAssertions;
using Genesis.Cdb.Challenge.Domain.Services;

namespace Genesis.Cdb.Challenge.UnitTests.Domain;

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
    [InlineData(6, 0.225)]
    [InlineData(12, 0.20)]
    [InlineData(24, 0.175)]
    [InlineData(25, 0.15)]
    public void Should_Apply_Correct_Tax_Rate(
        int months,
        decimal expectedTaxRate)
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

        decimal appliedTax =
            1 - netProfit / grossProfit;

        // Assert
        Math.Round(appliedTax, 3)
            .Should()
            .Be(Math.Round(expectedTaxRate, 3));
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
    public void Gross_Amount_Should_Increase_Month_Over_Month()
    {
        // Arrange
        decimal initialAmount = 1000m;

        // Act
        var result6Months =
            _calculator.Calculate(initialAmount, 6);

        var result12Months =
            _calculator.Calculate(initialAmount, 12);

        // Assert
        result12Months.GrossAmount
            .Should()
            .BeGreaterThan(result6Months.GrossAmount);
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
}