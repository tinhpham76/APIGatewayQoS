using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCircuitBreaker.Resilients
{
    public class RetryWithExponentialBackoff
    {
        private readonly int maxRetries, delayMilliseconds, maxDelayMilliseconds;

        public RetryWithExponentialBackoff(
            int maxRetries = 50,
            int delayMilliseconds = 200,
            int maxDelayMilliseconds = 2000)
        {
            this.maxRetries = maxRetries;
            this.delayMilliseconds = delayMilliseconds;
            this.maxDelayMilliseconds = maxDelayMilliseconds;
        }

        public async Task RunAsync(Func<Task> func)
        {
            ExponentialBackoff backoff = new ExponentialBackoff(this.maxRetries,
                this.delayMilliseconds,
                this.maxDelayMilliseconds);
        retry:
            try
            {
                await func();
            }
            catch (Exception ex) 
            //when (ex is TimeoutException ||
            //    ex is System.Net.Http.HttpRequestException)
            {
                //Console.WriteLine("Exception raised is: " +
                //    ex.GetType().ToString() +
                //    " –Message: " + ex.Message +
                //    " -- Inner Message: " +
                //    ex.InnerException.Message);
                await backoff.Delay();
                goto retry;
            }
        }
    }
}
