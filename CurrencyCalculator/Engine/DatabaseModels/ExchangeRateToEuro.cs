namespace Engine.DatabaseModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExchangeRateToEuro
    {
        [Key]
        [Required]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Rate { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}