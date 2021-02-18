namespace Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Engine.DatabaseModels;

    using Xunit;

    public class DatabaseModelsTests
    {
        [Fact]
        public void ExchangeRateToEuroShouldHaveCurrencyCode()
        {
            var rate = new ExchangeRateToEuro
            {
                CurrencyCode = null,
                Rate = 2m,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(rate, new ValidationContext(rate), validatorResults, true);


            Assert.False(actual);
            Assert.Single(validatorResults);
        }
    }
}