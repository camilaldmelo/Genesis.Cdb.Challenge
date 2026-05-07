using Genesis.Cdb.Challenge.Application.Commands;
using Genesis.Cdb.Challenge.Application.DTOs;
using Genesis.Cdb.Challenge.Application.Interfaces;
using MediatR;

namespace Genesis.Cdb.Challenge.Application.Handlers
{
    public class CalculateCdbCommandHandler : IRequestHandler<CalculateCdbCommand, CdbCalculationResponse>
    {

        private readonly ICdbCalculator _calculator;

        public CalculateCdbCommandHandler(
            ICdbCalculator calculator)
        {
            _calculator = calculator;
        }

        public async Task<CdbCalculationResponse> Handle(
            CalculateCdbCommand request,
            CancellationToken cancellationToken)
        {
            var result = _calculator.Calculate(
                request.InitialAmount,
                request.Months);

            return await Task.FromResult(result);
        }
    }
}
