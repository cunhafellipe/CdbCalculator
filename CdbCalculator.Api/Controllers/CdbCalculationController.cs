using CdbCalculator.Api.Dtos;
using CdbCalculator.Core.Domain;
using CdbCalculator.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CdbCalculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CdbCalculationController(ICdbCalculator cdbCalculator) : ControllerBase
    {
        private readonly ICdbCalculator _cdbCalculator = cdbCalculator;

        [HttpPost]
        public ActionResult<CdbCalculationResult> Calculate([FromBody] InvestmentDto investmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Investment investment = new(investmentDto.InitialValue, investmentDto.Months);
            CdbCalculationResult result = _cdbCalculator.Calculate(investment);
            return Ok(result);
        }
    }
}
