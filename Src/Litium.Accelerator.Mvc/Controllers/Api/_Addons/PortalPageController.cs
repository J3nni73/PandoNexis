using Litium.Media;
using Litium.Runtime;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.Accelerator.Extensions.Builders.Media;
using PandoNexis.AddOns.Extensions.PNPortalPage;
using System;
using System.Threading.Tasks;

namespace Litium.Accelerator.Mvc.Controllers.Api.Addons
{
    [Route("api/portalpage")]
    public class PortalPageController : ApiControllerBase
    {

        private readonly PortalPageService _portalPageService;
        private readonly ApplicationJsonConverter _applicationJsonConverter;

        public PortalPageController(PortalPageService portalPageService, ApplicationJsonConverter applicationJsonConverter)
        {
            _portalPageService = portalPageService;
            _applicationJsonConverter = applicationJsonConverter;
        }

        [HttpGet]
        [Route("getGetPortalPageData")]
        public async Task<IActionResult> GetPortalPageData(Guid PageSystemId)
        {
            var portalPageData = _portalPageService.BuildPortalPageData(PageSystemId);

            if (portalPageData != null)
            {
                try
                {
                    var returnData = _applicationJsonConverter.ConvertObjectToJson(portalPageData);
                    return Ok(returnData);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                
            }
            return BadRequest();
        }
    }
}