using Litium.Web.Models.Blocks;
using Microsoft.AspNetCore.Mvc;
using PandoNexis.AddOns.Extensions.Block.HeroBlock;

namespace Litium.Accelerator.Mvc.Controllers._Addons.PNBlockHero
{
    /// <summary>
    /// Represents the controller for video block.
    /// </summary>
    public class HeroBlockController : ViewComponent
    {
        private readonly HeroBlockViewModelBuilder _builder;

        public HeroBlockController(HeroBlockViewModelBuilder builder)
        {
            _builder = builder;
        }

        public IViewComponentResult Invoke(BlockModel currentBlockModel)
        {
            var model = _builder.Build(currentBlockModel);
            return View("~/Views/_Addons/Blocks/Hero.cshtml", model);
        }
    }
}