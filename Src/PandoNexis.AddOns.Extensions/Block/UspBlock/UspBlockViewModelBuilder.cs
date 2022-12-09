using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using PandoNexis.Accelerator.Extensions.Builders;

namespace PandoNexis.AddOns.Extensions.Block.UspBlock
{
    public class UspBlockViewModelBuilder : IViewModelBuilder<UspBlockViewModel>
    {
        private readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        private readonly UspBlockItemViewModelBuilder _uspBlockItemViewModelBuilder;


        public UspBlockViewModelBuilder(ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder,
                                                    UspBlockItemViewModelBuilder uspBlockItemViewModelBuilder)
        {
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
            _uspBlockItemViewModelBuilder = uspBlockItemViewModelBuilder;
        }

        /// <summary>
        /// Build the video block view model
        /// </summary>
        /// <param name="blockModel">The current block</param>
        /// <returns>Return the video block view model</returns>
        public virtual UspBlockViewModel Build(BlockModel blockModel)
        {
            var model = blockModel.MapTo<UspBlockViewModel>();
            model.Button = _extendedLinkViewModelBuilder.Build(blockModel);
            model.Items = _uspBlockItemViewModelBuilder.Build(blockModel);

            return model;
        }
    }
}
