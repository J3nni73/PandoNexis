using Litium.Accelerator.Builders.Block;
using Litium.Web.Models.Blocks;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.Block.ImageStatsBlock;

namespace Litium.Accelerator.Mvc.Controllers._Addons.PnBlockImageStats
{
    /// <summary>
    /// Represents the controller for video block.
    /// </summary>
    public class ImageStatsBlockController : ViewComponent
    {
        private readonly ImageStatsBlockViewModelBuilder _builder;

        public ImageStatsBlockController(ImageStatsBlockViewModelBuilder builder)
        {
            _builder = builder;
        }

        public IViewComponentResult Invoke(BlockModel currentBlockModel)
        {
            var model = _builder.Build(currentBlockModel);
            return View("~/Views/_Addons/Blocks/ImageStats.cshtml", model);
        }
    }
}