using AutoMapper;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using Litium.Accelerator.Builders;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.Accelerator.Extensions;
using PandoNexis.Accelerator.Extensions.ViewModels;

namespace PandoNexis.AddOns.Extensions.Block.ColumnBlock
{
    public class ColumnBlockViewModel : IViewModel, IAutoMapperConfiguration
    {
        public string BlockTitle { get; set; } = string.Empty;
        public string BlockSubTitle { get; set; } = string.Empty;
        public string BlockText { get; set; } = string.Empty;
        public string BlockText2 { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public List<ColumnBlockItemViewModel> Items { get; set; } = new List<ColumnBlockItemViewModel>();
        public ExtendedLinkViewModel Button { get; set; } = new ExtendedLinkViewModel();


        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BlockModel, ColumnBlockViewModel>()
               .ForMember(x => x.BlockTitle, m => m.MapFromField(BlockFieldNameConstants.BlockTitle))
               .ForMember(x => x.BlockSubTitle, m => m.MapFromField(BlockFieldNameConstants.BlockSubTitle))
               .ForMember(x => x.BlockText, m => m.MapFromField(BlockFieldNameConstants.BlockText))
               .ForMember(x => x.BlockText2, m => m.MapFromField(BlockFieldNameConstants.BlockText2))
               .ForMember(x => x.Summary, m => m.MapFromField(BlockFieldNameConstants.Summary))
             ;
        }
    }
}

