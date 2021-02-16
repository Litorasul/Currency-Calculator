namespace CurrencyCalculator
{
    using System;
    using System.Collections.Generic;

    public class ExchangeRate
    {
        public bool Success { get; set; }

        public DateTime Date { get; set; }

        public string Base { get; set; }

        public Dictionary<string, decimal> Rates { get; set; }
    }
}