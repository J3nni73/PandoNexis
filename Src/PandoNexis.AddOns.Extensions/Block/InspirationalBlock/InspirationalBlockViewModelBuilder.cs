using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using PandoNexis.Accelerator.Extensions.Builders;

namespace PandoNexis.AddOns.Extensions.Block.InspirationalBlock
{
    public class InspirationalBlockViewModelBuilder : IViewModelBuilder<InspirationalBlockViewModel>
    {
        private readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        private readonly InspirationalBlockItemViewModelBuilder _inspirationalBlockItemViewModelBuilder;

        public InspirationalBlockViewModelBuilder(ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder,
                                                    InspirationalBlockItemViewModelBuilder inspirationalBlockItemViewModelBuilder)
        {
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
            _inspirationalBlockItemViewModelBuilder = inspirationalBlockItemViewModelBuilder;
        }

        /// <summary>
        /// Build the video block view model
        /// </summary>
        /// <param name="blockModel">The current block</param>
        /// <returns>Return the video block view model</returns>
        public virtual InspirationalBlockViewModel Build(BlockModel blockModel)
        {
            var model = blockModel.MapTo<InspirationalBlockViewModel>();
            model.Button = _extendedLinkViewModelBuilder.Build(blockModel);
            model.Items = _inspirationalBlockItemViewModelBuilder.Build(blockModel);

            return model;
        }
    }
}
