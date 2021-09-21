using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCircuitBreaker.Interfaces
{
    public interface IHttpPolly
    {
        Task<string> CallApi();
    }
}
