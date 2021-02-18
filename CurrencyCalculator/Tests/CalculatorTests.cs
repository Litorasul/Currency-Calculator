namespace Tests
{
    using System;
    using System.Collections.Generic;

    using Engine.Models;
    using Engine;

    using Xunit;

    public class CalculatorTests
    {
        public Calculator Calculator { get; }

        public CalculatorTests()
        {
            var rate = new ExchangeRate
            {
                Base = "EUR",
                Date = DateTime.UtcNow,
                Success = true,
                Rates = new Dictionary<string, decimal>()
            };
            rate.Rates["BGN"] = 2m;
            rate.Rates["NOK"] = 10m;

            this.Calculator = new Calculator(rate);
        }

        [Theory]
        [InlineData("BGN", 20, 10)]
        [InlineData("NOK", 100, 10)]
        [InlineData("EUR", 23, 23)]
        public void ConvertToEuroShouldWorkCorrectly(string code, decimal amount, decimal expected)
        {
            var actual = this.Calculator.ConvertToEuro(code, amount);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("BGN", "NOK", 10, 50)]
        [InlineData("EUR", "BGN", 10, 20)]
        [InlineData("NOK", "BGN", 100, 20)]
        public void ConvertShouldWorkCorrectly(string from, string to, decimal amount, decimal expected)
        {
            var actual = this.Calculator.Convert(from, to, amount);

            Assert.Equal(expected, actual);
        }
    }
}
