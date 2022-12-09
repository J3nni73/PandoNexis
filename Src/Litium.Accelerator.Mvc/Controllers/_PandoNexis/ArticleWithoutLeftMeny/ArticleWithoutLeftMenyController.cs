using Litium.Web.Models.Websites;
using Litium.Accelerator.Builders.Article;
using Microsoft.AspNetCore.Mvc;

namespace Litium.Accelerator.Mvc.Controllers._PandoNexis.ArticleWithoutLeftMeny
{
    public class ArticleWithoutLeftMenyController : ControllerBase
    {
        private readonly ArticleViewModelBuilder _articleViewModelBuilder;

        public ArticleWithoutLeftMenyController(ArticleViewModelBuilder articleViewModelBuilder)
        {
            _articleViewModelBuilder = articleViewModelBuilder;
        }

        [HttpGet]
        public ActionResult Index(PageModel currentPageModel)
        {
            var model = _articleViewModelBuilder.Build(currentPageModel);
            return View(model);
        }
    }
}