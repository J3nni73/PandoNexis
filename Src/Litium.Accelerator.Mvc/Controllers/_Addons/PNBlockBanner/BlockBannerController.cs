using Litium.Web.Models.Blocks;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.Block.BlockBanner;

namespace Litium.Accelerator.Mvc.Controllers._Addons.PNBlockColumn
{
    /// <summary>
    /// Represents the controller for video block.
    /// </summary>
    public class BlockBannerController : ViewComponent
    {
        private readonly BlockBannerViewModelBuilder _builder;

        public BlockBannerController(BlockBannerViewModelBuilder builder)
        {
            _builder = builder;
        }

        public IViewComponentResult Invoke(BlockModel currentBlockModel)
        {
            var model = _builder.Build(currentBlockModel);
            return View("~/Views/_Addons/Blocks/Banner.cshtml", model);
        }
    }
}