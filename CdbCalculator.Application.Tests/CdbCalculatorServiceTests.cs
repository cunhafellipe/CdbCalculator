using Xunit;
using CdbCalculator.Core.Domain;
using CdbCalculator.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CdbCalculator.Application.Tests
{
    public class CdbCalculatorServiceTests
    {
        private readonly CdbCalculatorService _service;

        public CdbCalculatorServiceTests()
        {
            _service = new CdbCalculatorService();
        }

        [Theory]
        [MemberData(nameof(CalculateTestData))]
        public void Calculate_WithVariousInputs_ReturnsExpectedResults(decimal initialValue, int months, decimal expectedGrossValue, decimal expectedNetValue, decimal[] expectedMonthlyValues)
        {
            Investment investment = new (initialValue, months);

            CdbCalculationResult result = _service.Calculate(investment);

            Assert.Equal(expectedGrossValue, Math.Round(result.GrossValue, 2));
            Assert.Equal(expectedNetValue, Math.Round(result.NetValue, 2));
            Assert.Equal(expectedMonthlyValues.Length, result.MonthlyValues.Count);

            for (int i = 0; i < expectedMonthlyValues.Length; i++)
            {
                Assert.Equal(expectedMonthlyValues[i], Math.Round(result.MonthlyValues[i], 2));
            }
        }

        [Fact]
        public void Calculate_WithNegativeInitialValue_ThrowsArgumentException()
        {
            decimal initialValue = -1000m;
            int months = 12;

            Assert.Throws<ArgumentException>(() => new Investment(initialValue, months));
        }

        [Fact]
        public void Calculate_WithZeroInitialValue_ThrowsArgumentException()
        {
            decimal initialValue = 0m;
            int months = 12;

            Assert.Throws<ArgumentException>(() => new Investment(initialValue, months));
        }

        [Fact]
        public void Calculate_WithOneMonth_ThrowsArgumentException()
        {
            decimal initialValue = 1000m;
            int months = 1;

            Assert.Throws<ArgumentException>(() => new Investment(initialValue, months));
        }

        public static IEnumerable<object[]> CalculateTestData()
        {
            return
            [
                GenerateTestData(1000m, 12),
                GenerateTestData(500m, 6),
                GenerateTestData(2000m, 24)
            ];
        }

        private static object[] GenerateTestData(decimal initialValue, int months)
        {
            var monthlyValues = new List<decimal>();
            decimal grossValue = initialValue;
            decimal cdi = 0.009m;
            decimal tb = 1.08m;

            for (int i = 0; i < months; i++)
            {
                grossValue *= 1 + (cdi * tb);
                monthlyValues.Add(Math.Round(grossValue, 2));
            }

            decimal grossFinal = monthlyValues.LastOrDefault();
            decimal taxRate = GetTaxRate(months);
            decimal netValue = Math.Round(grossFinal - (grossFinal - initialValue) * taxRate, 2);

            return
            [
                initialValue,
                months,
                grossFinal,
                netValue,
                monthlyValues.ToArray()
            ];
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
