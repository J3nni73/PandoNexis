using Litium.Accelerator.Builders.Article;
using Litium.Web.Models.Websites;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.PNCollectionPage;

namespace Litium.Accelerator.Mvc.Controllers.Addons.CollectionPage
{
    public class CollectionPageController : ControllerBase
    {
        private readonly CollectionPageViewModelBuilder _collectionPageViewModelBuilder;

        public CollectionPageController(CollectionPageViewModelBuilder collectionPageViewModelBuilder)
        {
            _collectionPageViewModelBuilder = collectionPageViewModelBuilder;
        }

        [HttpGet]
        public ActionResult Index(PageModel currentPageModel)
        {
            var model = _collectionPageViewModelBuilder.Build(currentPageModel);
            return View(model);
        }
    }
}