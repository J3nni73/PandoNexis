using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using PandoNexis.Accelerator.Extensions.Builders;

namespace PandoNexis.AddOns.Extensions.Block.HeroBlock
{
    public class HeroBlockViewModelBuilder : IViewModelBuilder<HeroBlockViewModel>
    {
        private readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        private readonly HeroBlockItemViewModelBuilder _HeroBlockItemViewModelBuilder;


        public HeroBlockViewModelBuilder(ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder,
                                                    HeroBlockItemViewModelBuilder HeroBlockItemViewModelBuilder)
        {
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
            _HeroBlockItemViewModelBuilder = HeroBlockItemViewModelBuilder;
        }

        /// <summary>
        /// Build the video block view model
        /// </summary>
        /// <param name="blockModel">The current block</param>
        /// <returns>Return the video block view model</returns>
        public virtual HeroBlockViewModel Build(BlockModel blockModel)
        {
            var model = blockModel.MapTo<HeroBlockViewModel>();
            model.Button = _extendedLinkViewModelBuilder.Build(blockModel);

            return model;
        }
    }
}
