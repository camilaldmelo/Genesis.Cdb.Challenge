using FluentAssertions;
using Genesis.Cdb.Challenge.Application.Commands;
using Genesis.Cdb.Challenge.Application.Validators;

namespace Genesis.Cdb.Challenge.UnitTests.Application.Validators;

public class CalculateCdbCommandValidatorTests
{
    private readonly CalculateCdbCommandValidator _validator = new();

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Should_Reject_InitialAmount_Less_Than_Or_Equal_To_Zero(
        decimal initialAmount)
    {
        var command = new CalculateCdbCommand
        {
            InitialAmount = initialAmount,
            Months = 6
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(x => x.PropertyName == nameof(command.InitialAmount));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Reject_Months_Less_Than_Or_Equal_To_One(
        int months)
    {
        var command = new CalculateCdbCommand
        {
            InitialAmount = 1000,
            Months = months
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(x => x.PropertyName == nameof(command.Months));
    }

    [Fact]
    public void Should_Accept_Valid_Command()
    {
        var command = new CalculateCdbCommand
        {
            InitialAmount = 1000,
            Months = 6
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }
}