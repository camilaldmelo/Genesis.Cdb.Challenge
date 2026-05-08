using FluentAssertions;
using Genesis.Cdb.Challenge.Application.Commands;
using Genesis.Cdb.Challenge.Application.Handlers;
using Genesis.Cdb.Challenge.Domain.Services;

namespace Genesis.Cdb.Challenge.UnitTests.Application.Handlers;

public class CalculateCdbCommandHandlerTests
{
    [Fact]
    public async Task Should_Calculate_Cdb_Investment_Using_Real_Calculator()
    {
        // Arrange
        var calculator = new CdbCalculator();

        var handler = new CalculateCdbCommandHandler(
            calculator);

        var command = new CalculateCdbCommand
        {
            InitialAmount = 1000m,
            Months = 12
        };

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();

        result.GrossAmount
            .Should()
            .Be(1123.08m);

        result.NetAmount
          .Should()
          .Be(1098.47m);
    }
}