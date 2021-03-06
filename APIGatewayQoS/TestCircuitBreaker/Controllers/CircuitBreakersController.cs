using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestCircuitBreaker.Interfaces;
using TestCircuitBreaker.Resilients;

namespace TestCircuitBreaker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CircuitBreakersController : ControllerBase
    {
        private readonly IHttpPolly _httpPoly;
        private readonly IHttpPolly1 _httpPoly1;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CircuitBreakersController> _logger;
        public CircuitBreakersController(IHttpPolly1 httpPolly1,
            IHttpPolly httpPoly,
            ILogger<CircuitBreakersController> logger,
            HttpClient httpClient)
        {
            _httpPoly1 = httpPolly1;
            _httpPoly = httpPoly;
            _httpClient = httpClient;
            _logger = logger;
        }

        [HttpGet("resilient")]
        public async Task<IActionResult> Resilient()
        {
            _logger.LogInformation("API was calling.");
            var result = "";

            var retry = new RetryWithExponentialBackoff(5, 200, 2000);

            await retry.RunAsync(async ( ) =>
            {
                result = await _httpClient.GetStringAsync("http://localhost:5001/Errors");
            });

            return Ok(result);
        }

        [HttpGet("polly")]
        public async Task<IActionResult> Polly()
        {
            var result = await _httpPoly.CallApi();

            return Ok(result);
        }

        [HttpGet("polly1")]
        public async Task<IActionResult> Polly1()
        {
            var result = await _httpPoly1.CallApi();

            return Ok(result);
        }
    }
}
