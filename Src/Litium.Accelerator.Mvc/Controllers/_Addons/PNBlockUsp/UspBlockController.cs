using Litium.Accelerator.Builders.Block;
using Litium.Web.Models.Blocks;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.Block.InfoTileBlock;
using PandoNexis.AddOns.Extensions.Block.UspBlock;

namespace Litium.Accelerator.Mvc.Controllers._Addons.PNBlockUsp
{
    /// <summary>
    /// Represents the controller for video block.
    /// </summary>
    public class UspBlockController : ViewComponent
    {
        private readonly UspBlockViewModelBuilder _builder;

        public UspBlockController(UspBlockViewModelBuilder builder)
        {
            _builder = builder;
        }

        public IViewComponentResult Invoke(BlockModel currentBlockModel)
        {
            var model = _builder.Build(currentBlockModel);
            return View("~/Views/_Addons/Blocks/Usp.cshtml", model);
        }
    }
}