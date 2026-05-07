using FluentAssertions;
using Genesis.Cdb.Challenge.Application.Commands;
using Genesis.Cdb.Challenge.Application.DTOs;
using Genesis.Cdb.Challenge.Application.Handlers;
using Genesis.Cdb.Challenge.Application.Interfaces;
using Moq;

namespace Genesis.Cdb.Challenge.UnitTests.Application;

public class CalculateCdbCommandHandlerTests
{
    private readonly Mock<ICdbCalculator> _calculatorMock;

    private readonly CalculateCdbCommandHandler _handler;

    public CalculateCdbCommandHandlerTests()
    {
        _calculatorMock = new Mock<ICdbCalculator>();

        _handler = new CalculateCdbCommandHandler(
            _calculatorMock.Object);
    }

    [Fact]
    public async Task Should_Return_CdbCalculationResponse()
    {
        // Arrange
        var command = new CalculateCdbCommand
        {
            InitialAmount = 1000m,
            Months = 12
        };

        var expectedResponse =
            new CdbCalculationResponse
            {
                GrossAmount = 1120.50m,
                NetAmount = 1098.30m
            };

        _calculatorMock
            .Setup(x => x.Calculate(
                command.InitialAmount,
                command.Months))
            .Returns(expectedResponse);

        // Act
        var result = await _handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();

        result.GrossAmount
            .Should()
            .Be(expectedResponse.GrossAmount);

        result.NetAmount
            .Should()
            .Be(expectedResponse.NetAmount);
    }

    [Fact]
    public async Task Should_Call_Calculator_Once()
    {
        // Arrange
        var command = new CalculateCdbCommand
        {
            InitialAmount = 2000m,
            Months = 6
        };

        _calculatorMock
            .Setup(x => x.Calculate(
                command.InitialAmount,
                command.Months))
            .Returns(new CdbCalculationResponse());

        // Act
        await _handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        _calculatorMock.Verify(
            x => x.Calculate(
                command.InitialAmount,
                command.Months),
            Times.Once);
    }

    [Fact]
    public async Task Should_Return_Expected_Values()
    {
        // Arrange
        var command = new CalculateCdbCommand
        {
            InitialAmount = 5000m,
            Months = 24
        };

        var response =
            new CdbCalculationResponse
            {
                GrossAmount = 6200m,
                NetAmount = 5990m
            };

        _calculatorMock
            .Setup(x => x.Calculate(
                command.InitialAmount,
                command.Months))
            .Returns(response);

        // Act
        var result = await _handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(response);
    }
}