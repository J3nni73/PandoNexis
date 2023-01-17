using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solution.Extensions.PNPilot.Services;

namespace Litium.Accelerator.Mvc.Controllers.Api.Solution
{
    [Route("api/pandoNexis/pilot")]
    public class PilotController : ApiControllerBase
    {
        private readonly PilotCustomerService _pilotCustomerService;
        private readonly PilotAddOnService _pilotAddOnService;

        public PilotController(PilotCustomerService pilotCustomerService,
                                PilotAddOnService pilotAddOnService)
        {
            _pilotCustomerService = pilotCustomerService;
            _pilotAddOnService = pilotAddOnService;
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

        [HttpGet]
        [Route("addonexists/{addonid}")]
        public async Task<IActionResult> getaddon(string addOnId)
        {

            if (_pilotAddOnService.AddOnExists(addOnId))
                return Ok(true);
            else
                return Ok(false);
        }

    
        [HttpGet]
        [Route("registeraddon/{addonid}")]
        public async Task<IActionResult> RegisterAddon(string addOnId)
        {

            if (_pilotAddOnService.RegisterAddOn(addOnId))
                return Ok("Addon Is Registered");
            return BadRequest();
        }

        public class Aspen
        {
            public string Name { get; set; }
            public List<string> List { get; set; }
        }
    }
}
