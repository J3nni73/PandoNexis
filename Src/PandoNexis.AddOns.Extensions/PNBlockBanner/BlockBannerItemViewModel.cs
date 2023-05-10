using AutoMapper;
using Litium.Runtime.AutoMapper;
using Litium.Accelerator.Builders;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.FieldFramework;
using System.Globalization;
using Litium.Web.Models;
using PandoNexis.Accelerator.Extensions.ViewModels;

namespace PandoNexis.AddOns.Extensions.Block.BlockBanner
{
    public class BlockBannerItemViewModel : IViewModel, IAutoMapperConfiguration
    {
        public string BlockTitle { get; set; } = string.Empty;
        public string BlockText { get; set; } = string.Empty;
        public ImageModel? Image { get; set; }
        public ImageModel? MobileImage { get; set; }
        public ExtendedLinkViewModel Button { get; set; } = new ExtendedLinkViewModel();

        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg) => cfg.CreateMap<MultiFieldItem, BlockBannerItemViewModel>()
               .ForMember(x => x.BlockTitle, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.BlockTitle), CultureInfo.CurrentCulture)))
               .ForMember(x => x.BlockText, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.BlockText), CultureInfo.CurrentCulture)))
               .ForMember(x => x.Image, m => m.MapFrom(c => c.Fields.GetValue<Guid>(BlockFieldNameConstants.BlockImage).MapTo<ImageModel>()))
               .ForMember(x => x.MobileImage, m => m.MapFrom(c => c.Fields.GetValue<Guid>(BlockFieldNameConstants.BlockMobileImage).MapTo<ImageModel>()))
              ;
    }
}
