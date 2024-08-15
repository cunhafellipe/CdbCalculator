using CdbCalculator.Core.Domain;
using Xunit;
using System;
using System.Collections.Generic;

namespace CdbCalculator.Core.Tests
{
    public class InvestmentTests
    {
        [Fact]
        public void Constructor_WithValidInputs_CreatesInvestment()
        {
            decimal initialValue = 1000m;
            int months = 12;

            var investment = new Investment(initialValue, months);

            Assert.Equal(initialValue, investment.InitialValue);
            Assert.Equal(months, investment.Months);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Constructor_WithNonPositiveInitialValue_ThrowsArgumentException(decimal initialValue)
        {
            int months = 12;

            Assert.Throws<ArgumentException>(() => new Investment(initialValue, months));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Constructor_WithInvalidMonths_ThrowsArgumentException(int months)
        {
            decimal initialValue = 1000m;

            Assert.Throws<ArgumentException>(() => new Investment(initialValue, months));
        }
    }

    public class CdbCalculationResultTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            decimal grossValue = 1100m;
            decimal netValue = 1080m;
            var monthlyValues = new List<decimal> { 1000m, 1020m, 1040m, 1060m, 1080m, 1100m };

            var result = new CdbCalculationResult(grossValue, netValue, monthlyValues);

            Assert.Equal(grossValue, result.GrossValue);
            Assert.Equal(netValue, result.NetValue);
            Assert.Equal(monthlyValues, result.MonthlyValues);
        }
    }
}