using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWcfHost
{
    public class MyService : IWcfService
    {
        private const int SleepTime = 40;

        public WcfServiceResult GetResultsSync(Guid g)
        {
            System.Threading.Thread.Sleep(SleepTime);
            return new WcfServiceResult
            {
                Message = $"Sleeping for {SleepTime} ms and returning"
            };
        }
    }
}
