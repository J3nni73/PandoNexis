using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;

using PandoNexis.Accelerator.Extensions.Builders;

namespace PandoNexis.AddOns.Extensions.Block.ColumnBlock
{
    public class ColumnBlockViewModelBuilder : IViewModelBuilder<ColumnBlockViewModel>
    {
        private readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        private readonly ColumnBlockItemViewModelBuilder _columnBlockItemViewModelBuilder;


        public ColumnBlockViewModelBuilder(ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder,
                                                    ColumnBlockItemViewModelBuilder columnBlockItemViewModelBuilder)
        {
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
            _columnBlockItemViewModelBuilder = columnBlockItemViewModelBuilder;
        }

        /// <summary>
        /// Build the video block view model
        /// </summary>
        /// <param name="blockModel">The current block</param>
        /// <returns>Return the video block view model</returns>
        public virtual ColumnBlockViewModel Build(BlockModel blockModel)
        {
            var model = blockModel.MapTo<ColumnBlockViewModel>();
            model.Button = _extendedLinkViewModelBuilder.Build(blockModel);
            model.Items = _columnBlockItemViewModelBuilder.Build(blockModel);

            return model;
        }
    }
}
