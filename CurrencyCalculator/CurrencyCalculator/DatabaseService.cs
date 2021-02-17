namespace CurrencyCalculator
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Engine.DatabaseModels;
    using Engine.Models;

    public class DatabaseService
    {
        private readonly CurrencyDbContext dbContext;

        public DatabaseService(CurrencyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateExchangeRateAsync(string currencyCode, decimal exchangeRate)
        {
            var rate = new ExchangeRateToEuro
            {
                CurrencyCode = currencyCode,
                Rate = exchangeRate,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            await this.dbContext.ExchangeRates.AddAsync(rate);
            await this.dbContext.SaveChangesAsync();
        }

        public async  Task<bool> UpdateExchangeRateAsync(string currencyCode, decimal exchangeRate)
        {
            var rate = this.dbContext.ExchangeRates
                .FirstOrDefault(x =>
                x.CurrencyCode == currencyCode);

            if (rate != null)
            {
                rate.Rate = exchangeRate;
                rate.UpdatedOn = DateTime.UtcNow;
                await this.dbContext.SaveChangesAsync();
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task StoreDailyExchangeRatesAsync(ExchangeRate model)
        {
            foreach (var rate in model.Rates)
            {
                var isUpdated = await this.UpdateExchangeRateAsync(rate.Key, rate.Value);
                if (!isUpdated)
                {
                    await this.CreateExchangeRateAsync(rate.Key, rate.Value);
                }
            }
        }
    }
}