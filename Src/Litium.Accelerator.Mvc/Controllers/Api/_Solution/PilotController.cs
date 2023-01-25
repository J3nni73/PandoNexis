using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Solution.Extensions.PNPilot.Objects;
using Solution.Extensions.PNPilot.Services;

namespace Litium.Accelerator.Mvc.Controllers.Api.Solution
{
    [Route("api/pandoNexis/pilot")]
    public class PilotController : ApiControllerBase
    {
        private readonly PilotCustomerService _pilotCustomerService;
        private readonly PilotAddOnService _pilotAddOnService;
        private readonly WorkItemService _pilotItemService;
        private readonly TimeService _timeService;
        private readonly TimeTypeService _timeTypeService;
        private readonly TimeStatusService _timeStatusService;
        private readonly ItemTypeService _itemTypeService;
        private readonly ItemStatusService _itemStatusService;

        public PilotController(PilotCustomerService pilotCustomerService,
                                PilotAddOnService pilotAddOnService,
                                WorkItemService pilotItemService,
                                TimeService timeService,
                                TimeTypeService timeTypeService,
                                TimeStatusService timeStatusService,
                                ItemTypeService itemTypeService,
                                ItemStatusService itemStatusService)
        {
            _pilotCustomerService = pilotCustomerService;
            _pilotAddOnService = pilotAddOnService;
            _pilotItemService = pilotItemService;
            _timeService = timeService;
            _timeTypeService = timeTypeService;
            _timeStatusService = timeStatusService;
            _itemTypeService = itemTypeService;
            _itemStatusService = itemStatusService;
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
        [Route("getitemtypes")]
        public async Task<IActionResult> GetItemType()
        {

            var itemtypes = _itemTypeService.GetItemTypes();
            return Ok(JsonConvert.SerializeObject(itemtypes));

        }
        [HttpGet]
        [Route("getitemstatuses")]
        public async Task<IActionResult> GetItemStatuses()
        {

            var itemStatuses = _itemStatusService.GetItemStatuses();
            return Ok(JsonConvert.SerializeObject(itemStatuses));

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
        public async Task<IActionResult> AddWorkItem(string type)
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

        [HttpGet]
        [Route("gettimetypes")]
        public async Task<IActionResult> GetTimeType()
        {

            var timetypes = _timeTypeService.GetTimeTypes();
            return Ok(JsonConvert.SerializeObject(timetypes));

        }
        [HttpGet]
        [Route("gettimestatuses")]
        public async Task<IActionResult> GetTimeStatuses()
        {

            var timeStatuses = _timeStatusService.GetTimeStatuses();
            return Ok(JsonConvert.SerializeObject(timeStatuses));

        }

        [HttpGet]
        [Route("getalltime")]
        public async Task<IActionResult> GetAllTime()
        {

            var allTime = _timeService.GetAllTime();
            return Ok(JsonConvert.SerializeObject(allTime));

        }
        [HttpPost]
        [Route("addtime")]
        public async Task<IActionResult> AddTime(string type)
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
                if (_timeService.AddOrUpdateTime(result))
                    return Ok();
            }

            return BadRequest();
        }
    }
}
