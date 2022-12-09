using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using PandoNexis.Accelerator.Extensions.Builders;

namespace PandoNexis.AddOns.Extensions.Block.ImageStatsBlock
{
    public class ImageStatsBlockViewModelBuilder : IViewModelBuilder<ImageStatsBlockViewModel>
    {
        private readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        private readonly ImageStatsBlockItemViewModelBuilder _ImageStatsBlockItemViewModelBuilder;


        public ImageStatsBlockViewModelBuilder(ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder,
                                                    ImageStatsBlockItemViewModelBuilder ImageStatsBlockItemViewModelBuilder)
        {
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
            _ImageStatsBlockItemViewModelBuilder = ImageStatsBlockItemViewModelBuilder;
        }

        /// <summary>
        /// Build the video block view model
        /// </summary>
        /// <param name="blockModel">The current block</param>
        /// <returns>Return the video block view model</returns>
        public virtual ImageStatsBlockViewModel Build(BlockModel blockModel)
        {
            var model = blockModel.MapTo<ImageStatsBlockViewModel>();
            //model.Button = _ExtendedLinkViewModelBuilder.Build(blockModel);
            model.Items = _ImageStatsBlockItemViewModelBuilder.Build(blockModel);

            return model;
        }
    }
}
