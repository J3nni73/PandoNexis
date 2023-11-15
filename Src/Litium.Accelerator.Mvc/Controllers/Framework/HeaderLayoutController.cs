using Litium.Accelerator.ViewModels.Framework;
using Litium.Accelerator.Builders.Framework;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Routing;
using HeaderLayoutConstants = PandoNexis.Accelerator.Extensions.Constants.HeaderLayoutConstants;

namespace Litium.Accelerator.Mvc.Controllers.Framework
{
    /// <summary>
    /// LayoutController renders views for the layout (header,foter,BreadCrumbs)
    /// </summary>
    [Browsable(false)]
    public class HeaderLayoutController : ViewComponent
    {
        private readonly HeaderViewModelBuilder<HeaderViewModel> _headerViewModelBuilder;
        private readonly RequestModelAccessor _requestModelAccessor;

        public HeaderLayoutController(HeaderViewModelBuilder<HeaderViewModel> headerViewModelBuilder, RequestModelAccessor requestModelAccessor)
        {
            _headerViewModelBuilder = headerViewModelBuilder;
            _requestModelAccessor = requestModelAccessor;
        }

        /// <summary>
        /// Builds header for the site
        /// </summary>
        /// <returns>Return view for the header</returns>
        public IViewComponentResult Invoke()
        {
            var viewModel = _headerViewModelBuilder.Build();
            //PandoExtensions: begin
            var website = _requestModelAccessor.RequestModel.WebsiteModel;
            return View("~/Views/_Solution/Framework/Header.cshtml", viewModel);
            //PandoExtensions: end

            //return View("~/Views/Framework/Header.cshtml", viewModel);
        }
    }
}
