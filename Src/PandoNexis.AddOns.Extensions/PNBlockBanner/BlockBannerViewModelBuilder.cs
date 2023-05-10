using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;

using PandoNexis.Accelerator.Extensions.Builders;

namespace PandoNexis.AddOns.Extensions.Block.BlockBanner
{
    public class BlockBannerViewModelBuilder : IViewModelBuilder<BlockBannerViewModel>
    {
        private readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        private readonly BlockBannerItemViewModelBuilder _blockBannerItemViewModelBuilder;


        public BlockBannerViewModelBuilder(ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder,
                                                    BlockBannerItemViewModelBuilder blockBannerItemViewModelBuilder)
        {
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
            _blockBannerItemViewModelBuilder = blockBannerItemViewModelBuilder;
        }

        /// <summary>
        /// Build the video block view model
        /// </summary>
        /// <param name="blockModel">The current block</param>
        /// <returns>Return the video block view model</returns>
        public virtual BlockBannerViewModel Build(BlockModel blockModel)
        {
            var model = blockModel.MapTo<BlockBannerViewModel>();
            //model.Button = _extendedLinkViewModelBuilder.Build(blockModel);
            model.Items = _blockBannerItemViewModelBuilder.Build(blockModel);

            return model;
        }
    }
}
