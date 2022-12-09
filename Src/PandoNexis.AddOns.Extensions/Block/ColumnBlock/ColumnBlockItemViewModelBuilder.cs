using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.FieldFramework;
using Litium.Blocks;
using PandoNexis.Accelerator.Extensions.Builders;

namespace PandoNexis.AddOns.Extensions.Block.ColumnBlock
{
    public class ColumnBlockItemViewModelBuilder : IViewModelBuilder<ColumnBlockItemViewModel>
    {
        private readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        private readonly FieldTemplateService _fieldTemplateService;



        public ColumnBlockItemViewModelBuilder(ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder,
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
        public virtual ColumnBlockItemViewModel Build(MultiFieldItem Item)
        {
            var model = Item.MapTo<ColumnBlockItemViewModel>();
            //model.Button = _ExtendedLinkViewModelBuilder.Build(Item);

            return model;
        }
        public virtual List<ColumnBlockItemViewModel> Build(BlockModel block)
        {
            var blockItemViewModels = new List<ColumnBlockItemViewModel>();



            var blockTemplate = _fieldTemplateService.Get<FieldTemplateBase>(block.FieldTemplateSystemId);


            var items = block.GetValue<IList<MultiFieldItem>>(BlockFieldNameConstants.ColumnBlockItem);

            if (items != null)
            {
                blockItemViewModels.AddRange(items.Select(i => Build(i)));
            }

            return blockItemViewModels;
        }
    }
}
