using Litium.Web.Models.Websites;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.PNMediaCatalog.Builders;

namespace Litium.Accelerator.Mvc.Controllers.Addons.MediaCatalog
{
    public class MediaCatalogController : ControllerBase
    {
        private readonly MediaCatalogViewModelBuilder _mediaCatalogViewModelBuilder;

        public MediaCatalogController(MediaCatalogViewModelBuilder mediaCatalogViewModelBuilder)
        {
            _mediaCatalogViewModelBuilder = mediaCatalogViewModelBuilder;
        }

        [HttpGet]
        public ActionResult Index(PageModel currentPageModel)
        {
            var model = _mediaCatalogViewModelBuilder.Build(currentPageModel);
            return View(model);
        }
    }
}