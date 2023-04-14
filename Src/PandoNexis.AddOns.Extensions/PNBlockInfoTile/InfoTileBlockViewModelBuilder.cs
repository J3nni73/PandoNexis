using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using PandoNexis.Accelerator.Extensions.Builders;

namespace PandoNexis.AddOns.Extensions.Block.InfoTileBlock
{
    public class InfoTileBlockViewModelBuilder : IViewModelBuilder<InfoTileBlockViewModel>
    {
        private readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        private readonly InfoTileBlockItemViewModelBuilder _InfoTileBlockItemViewModelBuilder;


        public InfoTileBlockViewModelBuilder(ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder,
                                                    InfoTileBlockItemViewModelBuilder InfoTileBlockItemViewModelBuilder)
        {
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
            _InfoTileBlockItemViewModelBuilder = InfoTileBlockItemViewModelBuilder;
        }

        /// <summary>
        /// Build the video block view model
        /// </summary>
        /// <param name="blockModel">The current block</param>
        /// <returns>Return the video block view model</returns>
        public virtual InfoTileBlockViewModel Build(BlockModel blockModel)
        {
            var model = blockModel.MapTo<InfoTileBlockViewModel>();
            model.Button = _extendedLinkViewModelBuilder.Build(blockModel);
            model.Items = _InfoTileBlockItemViewModelBuilder.Build(blockModel);

            return model;
        }
    }
}
