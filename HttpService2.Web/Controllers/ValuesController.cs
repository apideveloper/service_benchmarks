using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HttpService2.Web.Controllers
{
    [RoutePrefix("api/service2")]
    public class ValuesController : ApiController
    {
        private const int SleepTime = 40;

        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri]Guid g)
        {
            await Task.Delay(SleepTime);
            var result = new
            {
                Message = $"Sleeping for {SleepTime} ms and returning"
            };

            return Ok(result);
        }
    }
}
