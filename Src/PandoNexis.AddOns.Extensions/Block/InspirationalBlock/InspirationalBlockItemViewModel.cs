using AutoMapper;
using Litium.Runtime.AutoMapper;
using Litium.Accelerator.Builders;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.FieldFramework;
using System.Globalization;
using Litium.FieldFramework.FieldTypes;
using Litium.Web.Models;
using PandoNexis.Accelerator.Extensions.ViewModels;

namespace PandoNexis.AddOns.Extensions.Block.InspirationalBlock
{
    public class InspirationalBlockItemViewModel : IViewModel, IAutoMapperConfiguration
    {

        public string BlockTitle { get; set; } = string.Empty;
        public string BlockSubTitle { get; set; } = string.Empty;
        public string BlockText { get; set; } = string.Empty;

        public string BlockSubTitle2 { get; set; } = string.Empty;
        public string BlockText2 { get; set; } = string.Empty;

        public bool Reverse { get; set; } = false;
        public bool NegativeMargin { get; set; } = false;
        public string Background { get; set; } = string.Empty;
        public bool FullWidth { get; set; } = false;

        public ImageModel? Image { get; set; }
        public FileModel? Video { get; set; }
        public ExtendedLinkViewModel Button { get; set; } = new ExtendedLinkViewModel();


        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<MultiFieldItem, InspirationalBlockItemViewModel>()
               .ForMember(x => x.BlockTitle, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.BlockTitle), CultureInfo.CurrentCulture)))
               .ForMember(x => x.BlockSubTitle, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.BlockSubTitle), CultureInfo.CurrentCulture)))
               .ForMember(x => x.BlockText, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.BlockText), CultureInfo.CurrentCulture)))
               .ForMember(x => x.Image, m => m.MapFrom(c => c.Fields.GetValue<Guid>(BlockFieldNameConstants.BlockImage, CultureInfo.CurrentUICulture).MapTo<ImageModel>()))
               .ForMember(x => x.Video, m => m.MapFrom(c => c.Fields.GetValue<PointerItem>(BlockFieldNameConstants.BlockVideo).EntitySystemId.MapTo<FileModel>()))
               .ForMember(x => x.BlockSubTitle2, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.BlockSubTitle2), CultureInfo.CurrentCulture)))
               .ForMember(x => x.BlockText2, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.BlockText2), CultureInfo.CurrentCulture)))
               .ForMember(x => x.Reverse, m => m.MapFrom(c => c.Fields.GetValue<bool>((BlockFieldNameConstants.Reverse))))
               .ForMember(x => x.NegativeMargin, m => m.MapFrom(c => c.Fields.GetValue<bool>((BlockFieldNameConstants.NegativeMargin))))
               .ForMember(x => x.FullWidth, m => m.MapFrom(c => c.Fields.GetValue<bool>((BlockFieldNameConstants.FullWidth))))
               .ForMember(x => x.Background, m => m.MapFrom(c => c.Fields.GetValue<string>((BlockFieldNameConstants.Background), CultureInfo.CurrentCulture)))
               ;
        }

    }
}
