using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestCircuitBreaker.Interfaces
{
    public class HttpPolly1 : IHttpPolly1
    {
        private const string apiUrl ="http://localhost:5001/api/errors";
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpPolly1> _logger;

        public HttpPolly1(HttpClient httpClient,
            ILogger<HttpPolly1> logger)
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
