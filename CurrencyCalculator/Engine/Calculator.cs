using Engine.Models;

namespace Engine
{
    public class Calculator
    {
        public ExchangeRate Rate { get; }

        public Calculator(ExchangeRate rate)
        {
            this.Rate = rate;
        }

        public decimal Convert(string fromCurrencyCode, string toCurrencyCode, decimal amount)
        {
            if (fromCurrencyCode == toCurrencyCode)
            {
                return amount;
            }

            var inEuro = this.ConvertToEuro(fromCurrencyCode, amount);
            return inEuro * this.Rate.Rates[toCurrencyCode];
        }

        private decimal ConvertToEuro( string currencyCode, decimal amount)
        {
            if (currencyCode == "EUR")
            {
                return amount;
            }

            return amount / this.Rate.Rates[currencyCode];
        }
    }
}