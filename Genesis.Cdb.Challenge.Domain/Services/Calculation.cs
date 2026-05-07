using Genesis.Cdb.Challenge.Application.DTOs;
using Genesis.Cdb.Challenge.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Cdb.Challenge.Domain.Services

{
    public class CdbCalculator : ICdbCalculator
    {
        private const decimal TB = 1.08m;
        private const decimal CDI = 0.009m;

        public CdbCalculationResponse Calculate(
            decimal initialAmount,
            int months)
        {
            decimal grossAmount = initialAmount;

            for (int i = 0; i < months; i++)
            {
                grossAmount *= (1 + (CDI * TB));
            }

            decimal profit = grossAmount - initialAmount;

            decimal tax = GetTax(months);

            decimal netAmount =
                grossAmount - (profit * tax);

            return new CdbCalculationResponse
            {
                GrossAmount = Math.Round(grossAmount, 2),
                NetAmount = Math.Round(netAmount, 2)
            };
        }

        private static decimal GetTax(int months)
        {
            return months switch
            {
                <= 6 => 0.225m,
                <= 12 => 0.20m,
                <= 24 => 0.175m,
                _ => 0.15m
            };
        }
    }
}
