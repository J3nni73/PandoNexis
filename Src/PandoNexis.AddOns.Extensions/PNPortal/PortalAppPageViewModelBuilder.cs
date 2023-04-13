using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Websites;
using PandoNexis.Accelerator.Extensions.Builders;

namespace PandoNexis.AddOns.Extensions.PNPortalPage
{
    public class PortalAppPageViewModelBuilder : IViewModelBuilder<PortalPageViewModel>
    {
        public readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        public readonly PortalPageService _PortalPageService;

        public PortalAppPageViewModelBuilder(
                                                ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder, 
                                                PortalPageService PortalPageService)
        {
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
            _PortalPageService = PortalPageService; 
        }
     
        /// <summary>
        /// Build the article model
        /// </summary>
        /// <param name="pageModel">The current article page</param>
        /// <returns>Return the article model</returns>
        public virtual PortalAppPageViewModel Build(PageModel pageModel)
        {
            var model = pageModel.MapTo<PortalAppPageViewModel>();
            model.PageId = pageModel.SystemId;
            return model;
        }
    }
}
