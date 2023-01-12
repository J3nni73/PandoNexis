using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Litium.Accelerator.Builders.Product;
using Litium.Accelerator.Builders.Search;
using Litium.Accelerator.Extensions;
using Litium.Accelerator.Mvc.Controllers.Api;
using Litium.Accelerator.Mvc.Extensions;
using Litium.Accelerator.Routing;
using Litium.Accelerator.ViewModels.Search;
using Litium.Runtime.AutoMapper;
using Litium.Web.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.PNInfinityScroll.Services;
using Solution.Extensions.PNPilot.Services;

namespace Litium.Accelerator.Mvc.Controllers.Api.Solution
{
    [Route("api/pandoNexis/pilot"), OnlyServiceAccountAuthorization]
    public class PilotController : ApiControllerBase
    {
        private readonly PilotCustomerService _pilotCustomerService;

        public PilotController(PilotCustomerService pilotCustomerService)
        {
            _pilotCustomerService = pilotCustomerService;
        }

      
        /// <summary>
        /// Gets available addons.
        /// </summary>
        [HttpGet]
        [Route("getavailableaddons")]
        public IActionResult GetAvailableAddons()
        {
            var test = new List<string>();
            test.Add("Hepp");
            test.Add("Hopp");
            test.Add("Hipp");
            test.Add("Hupp");
            test.Add("Happ");
            var ret = new Aspen
            {
                Name = "Aspenberg",
                List = test
            };
            return Ok(ret);
        }

        /// <summary>
        /// Gets available addons.
        /// </summary>
        [HttpGet]
        [Route("getcustomers")]
        public IActionResult GetCustomers()
        {
            var customers = _pilotCustomerService.GetCustomers();
           
            return Ok(customers);
        }

        [HttpGet]
        [Route("getaddonsforcustomers")]
        public IActionResult GetAddonsForCustomers()
        {
            var test = new List<string>();
            test.Add("Hepp");
            test.Add("Hopp");
            test.Add("Hipp");
            test.Add("Hupp");
            test.Add("Happ");
            var ret = new Aspen
            {
                Name = "Aspenberg",
                List = test
            };
            return Ok(ret);
        }
    }

    public class Aspen
    {
        public string Name { get; set; }
        public List<string> List { get; set; }
    }
}
