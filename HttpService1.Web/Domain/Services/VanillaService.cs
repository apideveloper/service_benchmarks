using System;
using System.Threading.Tasks;
using HttpService1.Web.Domain.Models;

namespace HttpService1.Web.Domain.Services
{
    public class VanillaService : IVanillaService
    {
        private const int SleepTime = 40;

        public VanillaResult GetResults(Guid g)
        {
            System.Threading.Thread.Sleep(SleepTime);
            return new VanillaResult
            {
                Message = $"Sleeping for {SleepTime} ms and returning"
            };
        }

        public async Task<VanillaResult> GetResultsAsync(Guid g)
        {
            await Task.Delay(SleepTime);
            return new VanillaResult
            {
                Message = $"Sleeping for {SleepTime} ms and returning"
            };
        }
    }
}