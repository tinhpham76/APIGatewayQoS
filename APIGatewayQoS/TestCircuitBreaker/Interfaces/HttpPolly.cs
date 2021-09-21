using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestCircuitBreaker.Interfaces
{
    public class HttpPolly : IHttpPolly
    {
        private const string apiUrl ="http://localhost:5001/api/errors";
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpPolly> _logger;

        public HttpPolly(HttpClient httpClient,
            ILogger<HttpPolly> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> CallApi()
        {
            var response = await _httpClient.GetAsync(apiUrl);
            _logger.LogDebug("[GetErrors] -> response code {StatusCode}", response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
