namespace CurrencyCalculator
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    using static GlobalConstants;

    public class CurrencyService
    {
        static HttpClient client = new HttpClient();

        public static async Task<ExchangeRate> GetLatestRateForEuro()
        {
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