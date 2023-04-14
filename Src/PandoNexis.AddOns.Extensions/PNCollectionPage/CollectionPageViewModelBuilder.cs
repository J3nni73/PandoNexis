using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Websites;
using PandoNexis.Accelerator.Extensions.Builders;

namespace PandoNexis.AddOns.Extensions.PNCollectionPage
{
    public class CollectionPageViewModelBuilder : IViewModelBuilder<CollectionPageViewModel>
    {
        public readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        public readonly CollectionPageService _collectionPageService;

        public CollectionPageViewModelBuilder(
                                                ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder, 
                                                CollectionPageService collectionPageService)
        {
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
            _collectionPageService = collectionPageService; 
        }
     
        /// <summary>
        /// Build the article model
        /// </summary>
        /// <param name="pageModel">The current article page</param>
        /// <returns>Return the article model</returns>
        public virtual CollectionPageViewModel Build(PageModel pageModel)
        {
            var model = pageModel.MapTo<CollectionPageViewModel>();
            model.PageId = pageModel.SystemId;
            return model;
        }
    }
}
