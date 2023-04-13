using AutoMapper;
using Litium.Runtime.AutoMapper;
using Litium.Accelerator.Builders;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.FieldFramework;
using System.Globalization;
using Litium.Web.Models;

namespace PandoNexis.AddOns.Extensions.Block.ColumnBlock
{
    public class ColumnBlockItemViewModel : IViewModel, IAutoMapperConfiguration
    {
        public string BlockTitle { get; set; } = string.Empty;
        public string BlockText { get; set; } = string.Empty;
        public ImageModel? Image { get; set; }

        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg) => cfg.CreateMap<MultiFieldItem, ColumnBlockItemViewModel>()
               .ForMember(x => x.BlockTitle, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.BlockTitle), CultureInfo.CurrentCulture)))
               .ForMember(x => x.BlockText, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.BlockText), CultureInfo.CurrentCulture)))
               .ForMember(x => x.Image, m => m.MapFrom(c => c.Fields.GetValue<Guid>(BlockFieldNameConstants.BlockImage).MapTo<ImageModel>()))
              ;
    }
}
