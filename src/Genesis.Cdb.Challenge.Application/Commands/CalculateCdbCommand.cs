using Genesis.Cdb.Challenge.Application.DTOs;
using MediatR;

namespace Genesis.Cdb.Challenge.Application.Commands
{ 
    public class CalculateCdbCommand
        : IRequest<CdbCalculationResponse>
    {
        public decimal InitialAmount { get; set; }

        public int Months { get; set; }
    }
}
