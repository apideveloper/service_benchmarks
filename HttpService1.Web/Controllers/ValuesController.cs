using Contracts;
using HttpService1.Web.Domain.Services;
using HttpService1.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Http;

namespace HttpService1.Web.Controllers
{
    [RoutePrefix("api/service1")]
    public class ValuesController : ApiController
    {
        #region iFX

        private static string HttpService2Uri = "http://localhost:1172/api/service2";
        private static string WcfUri = "net.tcp://localhost:8523/HelloWorldService";
        private static HttpClient Http = GetHttpClient();
        private static IVanillaService VanillaService = GetVanillaService();

        private IWcfService GetWcfClient()
        {
            NetTcpBinding binding = new NetTcpBinding();
            EndpointAddress address = new EndpointAddress(WcfUri);
            ChannelFactory<IWcfService> factory = new ChannelFactory<IWcfService>(binding, address);

            return factory.CreateChannel();
        }

        private static HttpClient GetHttpClient()
        {
            return new HttpClient();
        }

        private static IVanillaService GetVanillaService()
        {
            return new VanillaService();
        }

        #endregion

        private HttpService1ResultModel CreateApiResponseMessage(string serviceResponseMessage)
        {
            return new HttpService1ResultModel
            {
                Message = $"Service result message: {serviceResponseMessage}"
            };
        }

        #region Endpoints

        [Route("default")]
        public IHttpActionResult GetDefault()
        {
            return Ok();
        }

        [Route("http2HttpAsync")]
        public async Task<IHttpActionResult> GetHttp2HttpAsync()
        {
            var httpService2Response = await Http.GetAsync($"{HttpService2Uri}?g={Guid.NewGuid()}");
            httpService2Response.EnsureSuccessStatusCode();
            var contents = await httpService2Response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<HttpService2ResultModel>(contents);

            return Ok(CreateApiResponseMessage(result.Message));
        }

        [Route("http2HttpSync")]
        public IHttpActionResult GetHttp2HttpSync()
        {
            var httpService2Response = Http.GetAsync($"{HttpService2Uri}?g={Guid.NewGuid()}").Result;
            httpService2Response.EnsureSuccessStatusCode();
            var contents = httpService2Response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<HttpService2ResultModel>(contents);

            return Ok(CreateApiResponseMessage(result.Message));
        }

        [Route("http2WcfSync")]
        public IHttpActionResult GetHttp2WcfSync()
        {
            var result = GetWcfClient().GetResultsSync(Guid.NewGuid());

            return Ok(CreateApiResponseMessage(result.Message));
        }

        [Route("vanillaAsync")]
        public async Task<IHttpActionResult> GetVanillaAsync()
        {
            var result = await VanillaService.GetResultsAsync(Guid.NewGuid());

            return Ok(CreateApiResponseMessage(result.Message));
        }

        [Route("vanillaSync")]
        public IHttpActionResult GetVanillaSync()
        {
            var result = VanillaService.GetResults(Guid.NewGuid());

            return Ok(CreateApiResponseMessage(result.Message));
        }

        #endregion
    }
}
