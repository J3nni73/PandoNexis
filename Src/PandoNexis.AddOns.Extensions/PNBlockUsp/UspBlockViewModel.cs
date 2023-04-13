using AutoMapper;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using Litium.Accelerator.Builders;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.Accelerator.Extensions;
using PandoNexis.Accelerator.Extensions.ViewModels;

namespace PandoNexis.AddOns.Extensions.Block.UspBlock
{
    public class UspBlockViewModel : IViewModel, IAutoMapperConfiguration
    {
        public string BlockTitle { get; set; } = string.Empty;
        public string BlockSubTitle { get; set; } = string.Empty;
        public string BlockText { get; set; } = string.Empty;
        public string Background { get;set; } = string.Empty;
        public List<UspBlockItemViewModel> Items { get; set; } = new List<UspBlockItemViewModel>();
        public ExtendedLinkViewModel Button { get; set; } = new ExtendedLinkViewModel();

        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BlockModel, UspBlockViewModel>()
               .ForMember(x => x.BlockTitle, m => m.MapFromField(BlockFieldNameConstants.BlockTitle))
               .ForMember(x => x.BlockSubTitle, m => m.MapFromField(BlockFieldNameConstants.BlockSubTitle))
               .ForMember(x => x.BlockText, m => m.MapFromField(BlockFieldNameConstants.BlockText))
               .ForMember(x => x.Background, m => m.MapFromField(BlockFieldNameConstants.Background))
             ;
        }
    }
}

