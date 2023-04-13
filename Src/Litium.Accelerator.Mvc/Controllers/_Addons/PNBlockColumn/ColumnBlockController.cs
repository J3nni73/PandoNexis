using Litium.Web.Models.Blocks;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.Block.ColumnBlock;

namespace Litium.Accelerator.Mvc.Controllers._Addons.PNBlockColumn
{
    /// <summary>
    /// Represents the controller for video block.
    /// </summary>
    public class ColumnBlockController : ViewComponent
    {
        private readonly ColumnBlockViewModelBuilder _builder;

        public ColumnBlockController(ColumnBlockViewModelBuilder builder)
        {
            _builder = builder;
        }

        public IViewComponentResult Invoke(BlockModel currentBlockModel)
        {
            var model = _builder.Build(currentBlockModel);
            return View("~/Views/_Addons/Blocks/Column.cshtml", model);
        }
    }
}