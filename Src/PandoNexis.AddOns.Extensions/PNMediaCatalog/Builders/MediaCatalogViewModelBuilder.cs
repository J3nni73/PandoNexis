using Litium.Accelerator.Builders;
using Litium.Accelerator.ViewModels.Article;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Websites;
using PandoNexis.AddOns.Extensions.PNMediaCatalog.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNMediaCatalog.Builders
{
    public class MediaCatalogViewModelBuilder : IViewModelBuilder<MediaCatalogViewModel>
    {
        /// <summary>
        /// Build the article model
        /// </summary>
        /// <param name="pageModel">The current article page</param>
        /// <returns>Return the article model</returns>
        public virtual MediaCatalogViewModel Build(PageModel pageModel)
        {
            var model = pageModel.MapTo<MediaCatalogViewModel>();
            return model;
        }
    }
}
