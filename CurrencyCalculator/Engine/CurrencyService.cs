namespace Engine
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Engine.Models;

    using static GlobalConstants;

    public class CurrencyService
    {
        static HttpClient client = new HttpClient();

        public static async Task<Currencies> GetAvailableCurrencies()
        {
            // Your Access Key for https://fixer.io/ should be in a static class called "Access" in a constant string called "KEY".
            var path = $"{BASE_URL}{SYMBOLS_URL}{KEY_URL}{Access.KEY}";

            Currencies currencies = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                currencies = JsonConvert.DeserializeObject<Currencies>(json);
            }

            return currencies;
        }

        public static async Task<ExchangeRate> GetLatestRateForEuro()
        {
            // Your Access Key for https://fixer.io/ should be in a static class called "Access" in a constant string called "KEY".
            var path = $"{BASE_URL}{LATEST_URL}{KEY_URL}{Access.KEY}";

            ExchangeRate rate = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                rate = JsonConvert.DeserializeObject<ExchangeRate>(json);
            }

            return rate;
        }

        public static async Task<ExchangeRate> GetHistoricalRateForEuro(string date)
        {
            // Your Access Key for https://fixer.io/ should be in a static class called "Access" in a constant string called "KEY".
            var path = $"{BASE_URL}{date}{KEY_URL}{Access.KEY}";

            ExchangeRate rate = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                rate = JsonConvert.DeserializeObject<ExchangeRate>(json);
            }

            return rate;
        }
    }
}