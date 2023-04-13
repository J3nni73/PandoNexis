using AutoMapper;
using Litium.Runtime.AutoMapper;
using Litium.Accelerator.Builders;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.FieldFramework;
using System.Globalization;

namespace PandoNexis.AddOns.Extensions.Block.ImageStatsBlock
{
    public class ImageStatsBlockItemViewModel : IViewModel, IAutoMapperConfiguration
    {
        public string BlockText { get; set; } = string.Empty;
        public string BlockText2 { get; set; } = string.Empty;


        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MultiFieldItem, ImageStatsBlockItemViewModel>()
                .ForMember(x => x.BlockText, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.BlockText), CultureInfo.CurrentCulture)))
                .ForMember(x => x.BlockText2, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.BlockText2), CultureInfo.CurrentCulture)))
             ;
        }
    }
}
