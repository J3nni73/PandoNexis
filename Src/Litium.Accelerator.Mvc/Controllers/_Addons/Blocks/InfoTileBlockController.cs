using Litium.Accelerator.Builders.Block;
using Litium.Web.Models.Blocks;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.Block.InfoTileBlock;

namespace Litium.Accelerator.Mvc.Controllers.Addons.Blocks
{
    /// <summary>
    /// Represents the controller for video block.
    /// </summary>
    public class InfoTileBlockController : ViewComponent
    {
        private readonly InfoTileBlockViewModelBuilder _builder;

        public InfoTileBlockController(InfoTileBlockViewModelBuilder builder)
        {
            _builder = builder;
        }

        public IViewComponentResult Invoke(BlockModel currentBlockModel)
        {
            var model = _builder.Build(currentBlockModel);
            return View("~/Views/_Addons/Blocks/InfoTile.cshtml", model);
        }
    }
}