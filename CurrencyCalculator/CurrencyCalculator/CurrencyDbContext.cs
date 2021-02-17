namespace CurrencyCalculator
{
    using Microsoft.EntityFrameworkCore;
    using Engine.DatabaseModels;

    public class CurrencyDbContext : DbContext
    {
        public DbSet<ExchangeRateToEuro> ExchangeRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.\\SQLEXPRESS01;Database=CurrencyCalculator-Kelov;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}