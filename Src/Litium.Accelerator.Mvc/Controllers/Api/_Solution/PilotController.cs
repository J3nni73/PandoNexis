using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Solution.Extensions.PNPilot.Services;

namespace Litium.Accelerator.Mvc.Controllers.Api.Solution
{
    [Route("api/pandoNexis/pilot")]
    public class PilotController : ApiControllerBase
    {
        private readonly PilotCustomerService _pilotCustomerService;
        private readonly PilotAddOnService _pilotAddOnService;
        private readonly PilotItemService _pilotItemService;

        public PilotController(PilotCustomerService pilotCustomerService,
                                PilotAddOnService pilotAddOnService,
                                PilotItemService pilotItemService)
        {
            _pilotCustomerService = pilotCustomerService;
            _pilotAddOnService = pilotAddOnService;
            _pilotItemService = pilotItemService;
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
        [Route("getworkitems")]
        public IActionResult GetWorkItems()
        {
            var items = _pilotItemService.GetItems();

            return Ok(JsonConvert.SerializeObject(items));
        }
        [HttpPost]
        [Route("addworkitem")]
        public async Task<IActionResult> HandleFormData(string type)
        {

            HttpContext.Request.EnableBuffering();
            Request.Body.Position = 0;
            using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
            {
                var task = stream
                    .ReadToEndAsync()
                    .ContinueWith(t =>
                    {
                        var res = t.Result;
                        // TODO: Handle the post result!
                        return res;
                    });

                // await processing of the result
                task.Wait();
                var result = task.Result;
                if (_pilotItemService.AddOrUpdateItem(result))             
                    return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("addonexists")]
        public async Task<IActionResult> AddOnExists(string addOnId)
        {

            if (_pilotAddOnService.AddOnExists(addOnId))
                return Ok(true);
            else
                return Ok(false);
        }

    
        [HttpGet]
        [Route("registeraddon")]
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
