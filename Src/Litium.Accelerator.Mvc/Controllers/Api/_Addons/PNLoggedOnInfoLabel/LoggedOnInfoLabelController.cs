using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.PNLoggedOnInfoLabel.Builders;
using System.Threading.Tasks;

namespace Litium.Accelerator.Mvc.Controllers.Api._Addons.PNLoggedOnInfoLabel
{
    [Route("api/loggedoninfolabel")]
    public class LoggedOnInfoLabelController : ApiControllerBase
    {
        private readonly LoggedOnInfoLabelBuilder _loggedOnInfoLabelBuilder;

        public LoggedOnInfoLabelController(LoggedOnInfoLabelBuilder loggedOnInfoLabelBuilder)
        {
            _loggedOnInfoLabelBuilder = loggedOnInfoLabelBuilder;
        }

        [HttpGet]
        [Route("getpersoninfo")]
        public async Task<IActionResult> GetPersonInfo()
        {
            var person = await _loggedOnInfoLabelBuilder.BuildAsync();
            return Ok(person);
        }
    }
}