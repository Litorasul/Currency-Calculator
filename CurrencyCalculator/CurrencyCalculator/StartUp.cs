namespace CurrencyCalculator
{
    using System;
    using System.Threading.Tasks;

    class StartUp
    {
        private static Currencies availableCurrencies;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Loading...");
            availableCurrencies = await CurrencyService.GetAvailableCurrencies();

            var fromCurrencyCode = GetCurrencyCode("From");

            Console.WriteLine();

            var toCurrencyCode = GetCurrencyCode("To");

            Console.WriteLine();

            var amount = GetAmount();

            var result = await GetResult(fromCurrencyCode, toCurrencyCode, amount);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{amount} {fromCurrencyCode} is {result} {toCurrencyCode}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static string GetCurrencyCode(string type)
        {
            Console.WriteLine($"{type} what Currency?");
            Console.Write("Please type in Currency Code: ");
            Console.ForegroundColor = ConsoleColor.Green;
            var code = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            code = CheckCurrencyCode(code);
            return code;
        }

        private static string CheckCurrencyCode(string code)
        {
            if (code.Length != 3 || !availableCurrencies.symbols.ContainsKey(code.ToUpper()))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong Currency Code.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter Currency Code: ");
                Console.ForegroundColor = ConsoleColor.Green;
                code = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                code = CheckCurrencyCode(code);
            }

            return code.ToUpper();
        }

        private static decimal GetAmount()
        {
            decimal amount = 0.0m;
            Console.Write("Please type in amount: ");
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                amount = decimal.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong format, please type in Numbers!");
                Console.ForegroundColor = ConsoleColor.White;
                amount = GetAmount();
            }

            return amount;
        }

        private static async Task<decimal> GetResult(string from, string to, decimal amount)
        {
            bool isLatest = CheckIsLatest();

            ExchangeRate rate = null;
            Calculator calculator = null;

            if (isLatest)
            {
                rate = await CurrencyService.GetLatestRateForEuro();
                calculator = new Calculator(rate);

            }
            else
            {
                var date = GetDate();
                rate = await CurrencyService.GetHistoricalRateForEuro(date.ToString("yyyy-MM-dd"));
                calculator = new Calculator(rate);
            }
            return calculator.Convert(from, to, amount);
        }

        private static DateTime GetDate()
        {
            DateTime result;

            Console.Write("Please type in a day, a month, and a year: ");
            Console.ForegroundColor = ConsoleColor.Green;
            var dateString = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (DateTime.TryParse(dateString, out result))
            {
                return result;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input! Please use 'YYYY-MM-DD' format!");
                Console.ForegroundColor = ConsoleColor.White;
                GetDate();
            }

            return result;
        }

        private static bool CheckIsLatest()
        {
            Console.WriteLine("Do you want real-time exchange rate? Y/N ");
            Console.ForegroundColor = ConsoleColor.Green;
            var response = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (response.ToUpper()[0] == 'N')
            {
                return false;
            }
            else if (response.ToUpper()[0] == 'Y')
            {
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input! Please type 'Y' for Yes or 'N' for No.");
                Console.ForegroundColor = ConsoleColor.White;
                CheckIsLatest();
            }

            return true;
        }
    }
}
