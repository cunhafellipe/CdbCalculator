using CdbCalculator.Api.Controllers;
using CdbCalculator.Api.Dtos;
using CdbCalculator.Core.Domain;
using CdbCalculator.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace CdbCalculator.Api.Tests
{
    public class CdbCalculationControllerTests
    {
        private readonly Mock<ICdbCalculator> _mockCalculator;
        private readonly CdbCalculationController _controller;

        public CdbCalculationControllerTests()
        {
            _mockCalculator = new Mock<ICdbCalculator>();
            _controller = new CdbCalculationController(_mockCalculator.Object);
        }

        [Fact]
        public void Calculate_WithValidInput_ReturnsOkResult()
        {
            var investmentDto = new InvestmentDto { InitialValue = 1000, Months = 12 };
            var expectedResult = new CdbCalculationResult(1100m, 1080m, [1000m, 1010m, 1020m, 1030m, 1040m, 1050m, 1060m, 1070m, 1080m, 1090m, 1100m]);
            _mockCalculator.Setup(c => c.Calculate(It.IsAny<Investment>())).Returns(expectedResult);

            var actionResult = _controller.Calculate(investmentDto);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultValue = Assert.IsType<CdbCalculationResult>(okResult.Value);
            Assert.Equal(expectedResult.GrossValue, resultValue.GrossValue);
            Assert.Equal(expectedResult.NetValue, resultValue.NetValue);
            Assert.Equal(expectedResult.MonthlyValues, resultValue.MonthlyValues);
        }

        [Fact]
        public void Calculate_WithInvalidModel_ReturnsBadRequest()
        {
            var investmentDto = new InvestmentDto { InitialValue = -1000, Months = 0 };
            _controller.ModelState.AddModelError("InitialValue", "Initial value must be positive");
            _controller.ModelState.AddModelError("Months", "Months must be greater than 1");

            var actionResult = _controller.Calculate(investmentDto);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public void Calculate_CorrectlyMapsInvestmentDto()
        {
            // Arrange
            var investmentDto = new InvestmentDto { InitialValue = 1000, Months = 12 };
            Investment? capturedInvestment = null;
            _mockCalculator.Setup(c => c.Calculate(It.IsAny<Investment>()))
                .Callback<Investment>(inv => capturedInvestment = inv)
                .Returns(new CdbCalculationResult(1100m, 1080m, []));

            // Act
            _controller.Calculate(investmentDto);

            // Assert
            Assert.NotNull(capturedInvestment);
            Assert.Equal(investmentDto.InitialValue, capturedInvestment.InitialValue);
            Assert.Equal(investmentDto.Months, capturedInvestment.Months);
        }
    }
}