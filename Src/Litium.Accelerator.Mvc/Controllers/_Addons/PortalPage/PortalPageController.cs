using Litium.Web.Models.Websites;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.PNPortalPage;

namespace Litium.Accelerator.Mvc.Controllers.Addons.PortalPage
{
    public class PortalPageController : ControllerBase
    {
        private readonly PortalPageViewModelBuilder _portalPageViewModelBuilder;

        public PortalPageController(PortalPageViewModelBuilder portalPageViewModelBuilder)
        {
            _portalPageViewModelBuilder = portalPageViewModelBuilder;
        }

        [HttpGet]
        public ActionResult Index(PageModel currentPageModel)
        {
            var model = _portalPageViewModelBuilder.Build(currentPageModel);
            return View(model);
        }
    }
}