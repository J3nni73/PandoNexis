using Litium.Accelerator.Builders;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.FieldFramework;
using Litium.Blocks;
using PandoNexis.Accelerator.Extensions.Builders;

namespace PandoNexis.AddOns.Extensions.Block.InfoTileBlock
{
    public class InfoTileBlockItemViewModelBuilder : IViewModelBuilder<InfoTileBlockItemViewModel>
    {
        private readonly ExtendedLinkViewModelBuilder _extendedLinkViewModelBuilder;
        private readonly FieldTemplateService _fieldTemplateService;



        public InfoTileBlockItemViewModelBuilder(ExtendedLinkViewModelBuilder extendedLinkViewModelBuilder,
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
        public virtual InfoTileBlockItemViewModel Build(MultiFieldItem Item)
        {
            var model = Item.MapTo<InfoTileBlockItemViewModel>();
            model.Button = _extendedLinkViewModelBuilder.Build(Item);

            return model;
        }
        public virtual List<InfoTileBlockItemViewModel> Build(BlockModel block)
        {
            var blockItemViewModels = new List<InfoTileBlockItemViewModel>();



            var blockTemplate = _fieldTemplateService.Get<FieldTemplateBase>(block.FieldTemplateSystemId);


            var items = block.GetValue<IList<MultiFieldItem>>(BlockFieldNameConstants.InfoTileBlockItem);

            if (items != null)
            {
                blockItemViewModels.AddRange(items.Select(i => Build(i)));
            }

            return blockItemViewModels;
        }
    }
}
