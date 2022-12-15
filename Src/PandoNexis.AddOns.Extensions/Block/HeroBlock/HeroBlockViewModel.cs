using AutoMapper;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using JetBrains.Annotations;
using Litium.Accelerator.Builders;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.Accelerator.Extensions;
using Litium.Web.Models;
using System.Globalization;
using PandoNexis.Accelerator.Extensions.ViewModels;

namespace PandoNexis.AddOns.Extensions.Block.HeroBlock
{
    public class HeroBlockViewModel : IViewModel, IAutoMapperConfiguration
    {
        public string BlockTitle { get; set; } = string.Empty;
        public string BlockTitle2 { get; set; } = string.Empty;
        public string BlockSubTitle { get; set; } = string.Empty;
        public string BlockText { get; set; } = string.Empty;

        public ImageModel BlockImage { get; set; } = new ImageModel();
        public ImageModel BlockMobilImage { get; set; } = new ImageModel();
        public ImageModel BlockOverlayImage { get; set; } = new ImageModel();

        public bool BlockOverlayLeft { get; set; } = false;
        public bool QuoteStyle { get; set; } = false;
        public bool FullWidth { get; set; } = false;
        public bool GradiantOverlay { get; set; } = false;
        public bool CenteredText { get; set; } = false;
        public string Background { get; set; } = string.Empty;

        public ExtendedLinkViewModel Button { get; set; } = new ExtendedLinkViewModel();

        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BlockModel, HeroBlockViewModel>()
               .ForMember(x => x.BlockTitle, m => m.MapFromField(BlockFieldNameConstants.BlockTitle))
               .ForMember(x => x.BlockTitle2, m => m.MapFromField(BlockFieldNameConstants.BlockTitle2))
               .ForMember(x => x.BlockSubTitle, m => m.MapFromField(BlockFieldNameConstants.BlockSubTitle))
               .ForMember(x => x.BlockText, m => m.MapFromField(BlockFieldNameConstants.BlockText))
               .ForMember(x => x.BlockImage, m => m.MapFrom(c => c.Fields.GetValue<Guid>(BlockFieldNameConstants.BlockImage).MapTo<ImageModel>()))
               .ForMember(x => x.BlockMobilImage, m => m.MapFrom(c => c.Fields.GetValue<Guid>(BlockFieldNameConstants.BlockMobilImage, CultureInfo.CurrentCulture).MapTo<ImageModel>()))
               .ForMember(x => x.BlockOverlayImage, m => m.MapFrom(c => c.Fields.GetValue<Guid>(BlockFieldNameConstants.BlockOverlayImage).MapTo<ImageModel>()))

               .ForMember(x => x.BlockOverlayLeft, m => m.MapFromField(BlockFieldNameConstants.BlockOverlayLeft))
               .ForMember(x => x.QuoteStyle, m => m.MapFromField(BlockFieldNameConstants.QuoteStyle))
               .ForMember(x => x.FullWidth, m => m.MapFromField(BlockFieldNameConstants.FullWidth))
               .ForMember(x => x.GradiantOverlay, m => m.MapFromField(BlockFieldNameConstants.GradiantOverlay))
               .ForMember(x => x.CenteredText, m => m.MapFromField(BlockFieldNameConstants.CenteredText))
               .ForMember(x => x.Background, m => m.MapFromField(BlockFieldNameConstants.Background))
             ;
        }
    }
}

