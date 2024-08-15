using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdbCalculator.Core.Domain
{
    public class CdbCalculationResult(decimal grossValue, decimal netValue, List<decimal> monthlyValues)
    {
        public decimal GrossValue { get; set; } = grossValue;
        public decimal NetValue { get; set; } = netValue;
        public List<decimal> MonthlyValues { get; set; } = monthlyValues;
    }
}