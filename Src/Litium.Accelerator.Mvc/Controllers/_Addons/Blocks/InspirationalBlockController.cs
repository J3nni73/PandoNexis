using Litium.Accelerator.Builders.Block;
using Litium.Web.Models.Blocks;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.Block.InspirationalBlock;

namespace Litium.Accelerator.Mvc.Controllers.Addons.Blocks
{
    /// <summary>
    /// Represents the controller for video block.
    /// </summary>
    public class InspirationalBlockController : ViewComponent
    {
        private readonly InspirationalBlockViewModelBuilder _builder;

        public InspirationalBlockController(InspirationalBlockViewModelBuilder builder)
        {
            _builder = builder;
        }

        public IViewComponentResult Invoke(BlockModel currentBlockModel)
        {
            var model = _builder.Build(currentBlockModel);
            return View("~/Views/_Addons/Blocks/Inspirational.cshtml", model);
        }
    }
}