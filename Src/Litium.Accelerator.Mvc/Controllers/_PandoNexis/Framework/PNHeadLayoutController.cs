using System.ComponentModel;
using System.Threading.Tasks;
using Litium.Accelerator.Builders.Framework;
using Litium.Accelerator.ViewModels.Framework;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.Accelerator.Extensions.Builders.Framework;
using PandoNexis.Accelerator.Extensions.ViewModels.Framework;

namespace Litium.Accelerator.Mvc.Controllers._PandoNexis.Framework
{
    /// <summary>
    /// LayoutController renders views for the layout (header,foter,BreadCrumbs)
    /// </summary>
    [Browsable(false)]
    public class PNHeadLayoutController : ViewComponent
    {
        private readonly HeadViewModelBuilder<HeadViewModel> _headViewModelBuilder;

        public PNHeadLayoutController(HeadViewModelBuilder<HeadViewModel> headViewModelBuilder)
        {
            _headViewModelBuilder = headViewModelBuilder;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = _headViewModelBuilder.Build();
            return View("~/Views/_PandoNexis/Framework/PNHead.cshtml", viewModel);
        }
    }
}
