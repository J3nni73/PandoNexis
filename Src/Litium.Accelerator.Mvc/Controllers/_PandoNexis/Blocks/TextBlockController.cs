using Litium.Accelerator.Builders.Block;
using Litium.Web.Models.Blocks;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.Accelerator.Extensions.Blocks.TextBlock;

namespace Litium.Accelerator.Mvc.Controllers.PandoNexis.Blocks
{
    /// <summary>
    /// Represents the controller for video block.
    /// </summary>
    public class TextBlockController : ViewComponent
    {
        private readonly TextBlockViewModelBuilder _builder;

        public TextBlockController(TextBlockViewModelBuilder builder)
        {
            _builder = builder;
        }

        public IViewComponentResult Invoke(BlockModel currentBlockModel)
        {
            var model = _builder.Build(currentBlockModel);
            return View("~/Views/_PandoNexis/Blocks/TextBlock.cshtml", model);
        }
    }
}