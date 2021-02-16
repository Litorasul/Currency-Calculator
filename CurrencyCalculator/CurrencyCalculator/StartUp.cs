namespace CurrencyCalculator
{
    using System;
    using System.Threading.Tasks;

    using static GlobalConstants;

    class StartUp
    {
        static async Task Main(string[] args)
        {
            var res = await CurrencyService.GetHistoricalRateForEuro("2020-01-12");

            Console.WriteLine(res.Base);
            Console.WriteLine(res.Date.DayOfWeek);
            Console.WriteLine(res.Rates["BGN"]);
        }
    }
}
