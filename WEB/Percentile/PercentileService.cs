using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WEB
{
    public class PercentileService : IPercentileService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public PercentileService(IConfiguration config, IHttpClientFactory httpClientFactory) 
        {
            this._httpClientFactory = httpClientFactory;
            this._config = config;
        }

        public async Task<double> GetPercentile(double percentile)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(string.Format("{0}/api/percentile/{1}", this._config.GetSection("ApiBaseUrl").Value, percentile));

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<double>();
        }
    }
}
