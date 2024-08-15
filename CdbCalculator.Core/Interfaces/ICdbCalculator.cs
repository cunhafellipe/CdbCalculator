using CdbCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdbCalculator.Core.Interfaces
{
    public interface ICdbCalculator
    {
        CdbCalculationResult Calculate(Investment investment);
    }
}