
using Litium.Web.Models.Websites;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Builders;

namespace Litium.Accelerator.Mvc.Controllers._Addons.GenericGridView
{
    public class GenericGridViewController : ControllerBase
    {
        private readonly GenericGridViewModelBuilder _genericGridViewModelBuilder;
        public GenericGridViewController(GenericGridViewModelBuilder genericGridViewModelBuilder)
        {
            _genericGridViewModelBuilder = genericGridViewModelBuilder;
        }
        [HttpGet]
        public ActionResult Index(PageModel currentPageModel)

        {
            var model = _genericGridViewModelBuilder.Build(currentPageModel);

            return View(model);
        }
    }
}
