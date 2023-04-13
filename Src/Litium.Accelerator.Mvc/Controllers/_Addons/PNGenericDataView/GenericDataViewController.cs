
using Litium.Web.Models.Websites;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Builders;

namespace Litium.Accelerator.Mvc.Controllers._Addons.GenericDataView
{
    public class GenericDataViewController : ControllerBase
    {
        private readonly GenericDataViewModelBuilder _genericDataViewModelBuilder;
        public GenericDataViewController(GenericDataViewModelBuilder genericDataViewModelBuilder)
        {
            _genericDataViewModelBuilder = genericDataViewModelBuilder;
        }
        [HttpGet]
        public ActionResult Index(PageModel currentPageModel)

        {
            var model = _genericDataViewModelBuilder.Build(currentPageModel);

            return View(model);
        }
    }
}
