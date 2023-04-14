using AutoMapper;
using Litium.Runtime.AutoMapper;
using JetBrains.Annotations;
using Litium.Accelerator.Builders;
using Litium.FieldFramework;
using System.Globalization;
using PandoNexis.Accelerator.Extensions.ViewModels;
using PandoNexis.Accelerator.Extensions.Constants;

namespace PandoNexis.Accelerator.Extensions.Blocks.TextBlock
{
    public class TextBlockItemViewModel : IViewModel, IAutoMapperConfiguration
    {

        public string BlockTitle { get; set; } = string.Empty;
        public string BlockSubTitle { get; set; } = string.Empty;
        public string BlockText { get; set; } = string.Empty;
        public string Background { get; set; } = string.Empty;
      
        public ExtendedLinkViewModel Button { get; set; } = new ExtendedLinkViewModel();


        [UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MultiFieldItem, TextBlockItemViewModel>()
               .ForMember(x => x.BlockTitle, m => m.MapFrom(c => c.Fields.GetValue<string>(BlockFieldNameConstants.BlockTitle, CultureInfo.CurrentCulture)))
               .ForMember(x => x.BlockSubTitle, m => m.MapFrom(c => c.Fields.GetValue<string>(BlockFieldNameConstants.BlockSubTitle, CultureInfo.CurrentCulture)))
               .ForMember(x => x.BlockText, m => m.MapFrom(c => c.Fields.GetValue<string>(BlockFieldNameConstants.BlockText, CultureInfo.CurrentCulture)))
               .ForMember(x => x.Background, m => m.MapFrom(c => c.Fields.GetValue<string>(BlockFieldNameConstants.Background)))
               ;
        }

    }
}
