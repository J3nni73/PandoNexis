using Litium.Accelerator.Builders;
using Litium.Accelerator.ViewModels.Block;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using Litium.FieldFramework;
using Litium.Blocks;
using PandoNexis.Accelerator.Extensions.Builders;
using PandoNexis.Accelerator.Extensions.Constants;

namespace PandoNexis.Accelerator.Extensions.Blocks.TextBlock
{
    public class TextBlockItemViewModelBuilder : IViewModelBuilder<TextBlockItemViewModel>
    {
        private readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        private readonly FieldTemplateService _fieldTemplateService;

        public TextBlockItemViewModelBuilder(ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder,
                                                        FieldTemplateService fieldTemplateService)
        {
            _extendedLinkViewModelBuilder = extendedLinkViewModelBuilder;
            _fieldTemplateService = fieldTemplateService;
        }

        /// <summary>
        /// Build the video block view model
        /// </summary>
        /// <param name="blockModel">The current block</param>
        /// <returns>Return the video block view model</returns>
        public virtual TextBlockItemViewModel Build(MultiFieldItem Item)
        {
            var model = Item.MapTo<TextBlockItemViewModel>();
            model.Button = _extendedLinkViewModelBuilder.Build(Item);

            return model;
        }
        public virtual List<TextBlockItemViewModel> Build(BlockModel block)
        {
            var blockItemViewModels = new List<TextBlockItemViewModel>();



            var blockTemplate = _fieldTemplateService.Get<FieldTemplateBase>(block.FieldTemplateSystemId);


            var items = block.GetValue<IList<MultiFieldItem>>(BlockFieldNameConstants.TextBlockItem);

            if (items != null)
            {
                blockItemViewModels.AddRange(items.Select(i => Build(i)));
            }

            return blockItemViewModels;
        }
    }
}
