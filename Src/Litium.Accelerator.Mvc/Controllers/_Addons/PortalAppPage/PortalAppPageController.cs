using Litium.Web.Models.Websites;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.PNPortalPage;

namespace Litium.Accelerator.Mvc.Controllers.Addons.PortalPage
{
    public class PortalAppPageController : ControllerBase
    {
        private readonly PortalAppPageViewModelBuilder _portalAppPageViewModelBuilder;

        public PortalAppPageController(PortalAppPageViewModelBuilder portalAppPageViewModelBuilder)
        {
            _portalAppPageViewModelBuilder = portalAppPageViewModelBuilder;
        }

        [HttpGet]
        public ActionResult Index(PageModel currentPageModel)
        {
            var model = _portalAppPageViewModelBuilder.Build(currentPageModel);
            return View(model);
        }
    }
}