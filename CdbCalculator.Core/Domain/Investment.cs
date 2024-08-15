using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdbCalculator.Core.Domain
{
    public class Investment
    {
        public decimal InitialValue { get; }
        public int Months { get; }

        public Investment(decimal initialValue, int months)
        {
            if (initialValue <= 0)
                throw new ArgumentException("O valor inicial deve ser maior que zero.", nameof(initialValue));
            if (months < 2)
                throw new ArgumentException("O número de meses deve ser maior ou igual a 2.", nameof(months));

            InitialValue = initialValue;
            Months = months;
        }
    }
}