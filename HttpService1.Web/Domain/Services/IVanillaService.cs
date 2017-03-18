using HttpService1.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpService1.Web.Domain.Services
{
    public interface IVanillaService
    {
        VanillaResult GetResults(Guid g);
        
        Task<VanillaResult> GetResultsAsync(Guid g);
    }
}
