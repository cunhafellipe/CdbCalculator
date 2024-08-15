using CdbCalculator.Core.Domain;
using CdbCalculator.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace CdbCalculator.Application.Services
{
    public class CdbCalculatorService : ICdbCalculator
    {
        private const decimal TB = 1.08m;
        private const decimal CDI = 0.009m;

        public CdbCalculationResult Calculate(Investment investment)
        {
            var monthlyValues = new List<decimal>();
            decimal grossValue = investment.InitialValue;

            for (int i = 0; i < investment.Months; i++)
            {
                grossValue *= 1 + (CDI * TB);
                monthlyValues.Add(Math.Round(grossValue, 2));
            }

            decimal taxRate = GetTaxRate(investment.Months);
            decimal grossFinal = Math.Round(grossValue, 2);
            decimal netValue = Math.Round(grossFinal - (grossFinal - investment.InitialValue) * taxRate, 2);

            return new CdbCalculationResult(
                grossFinal,
                netValue,
                monthlyValues
            );
        }

        private static decimal GetTaxRate(int months)
        {
            if (months <= 6) return 0.225m;
            if (months <= 12) return 0.20m;
            if (months <= 24) return 0.175m;
            return 0.15m;
        }
    }
}
