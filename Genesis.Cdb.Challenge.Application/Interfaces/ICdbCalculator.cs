
using Genesis.Cdb.Challenge.Application.DTOs;

namespace Genesis.Cdb.Challenge.Application.Interfaces
{
    public interface ICdbCalculator
    {
        CdbCalculationResponse Calculate(
            decimal initialAmount,
            int months);
    }

}
