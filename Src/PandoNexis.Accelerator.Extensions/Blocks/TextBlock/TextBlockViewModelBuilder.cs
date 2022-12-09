using Litium.Accelerator.Builders;
using Litium.Accelerator.ViewModels.Block;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using System.Globalization;
using PandoNexis.Accelerator.Extensions.Builders;
using PandoNexis.Accelerator.Extensions.Constants;

namespace PandoNexis.Accelerator.Extensions.Blocks.TextBlock
{
    public class TextBlockViewModelBuilder : IViewModelBuilder<TextBlockViewModel>
    {
        private readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        private readonly TextBlockItemViewModelBuilder _textBlockItemViewModelBuilder;

        public TextBlockViewModelBuilder(ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder,
                                                    TextBlockItemViewModelBuilder textBlockItemViewModelBuilder)
        {
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
            _textBlockItemViewModelBuilder = textBlockItemViewModelBuilder;
        }

        /// <summary>
        /// Build the video block view model
        /// </summary>
        /// <param name="blockModel">The current block</param>
        /// <returns>Return the video block view model</returns>
        public virtual TextBlockViewModel Build(BlockModel blockModel)
        {
            var model = new TextBlockViewModel();
            model.Background = blockModel.GetValue<string>(BlockFieldNameConstants.Background);
            model.Items = _textBlockItemViewModelBuilder.Build(blockModel);

            return model;
        }
    }
}
