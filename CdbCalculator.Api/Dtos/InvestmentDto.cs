using System.ComponentModel.DataAnnotations;

namespace CdbCalculator.Api.Dtos
{
    public class InvestmentDto
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal InitialValue { get; set; }

        [Required]
        [Range(2, int.MaxValue)]
        public int Months { get; set; }
    }
}