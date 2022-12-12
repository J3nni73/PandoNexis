using AutoMapper;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using Litium.Accelerator.Builders;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.Accelerator.Extensions;
using Litium.Web.Models;
using System.Globalization;

namespace PandoNexis.AddOns.Extensions.Block.ImageStatsBlock
{
    public class ImageStatsBlockViewModel : IViewModel, IAutoMapperConfiguration
    {



        public string BlockTitle { get; set; } = string.Empty;
        public string BlockTitle2 { get; set; } = string.Empty;
        public ImageModel Image { get; set; } = new ImageModel();
        public List<ImageStatsBlockItemViewModel> Items { get; set; } = new List<ImageStatsBlockItemViewModel>();


        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BlockModel, ImageStatsBlockViewModel>()
              .ForMember(x => x.BlockTitle, m => m.MapFromField(BlockFieldNameConstants.BlockTitle))
              .ForMember(x => x.BlockTitle2, m => m.MapFromField(BlockFieldNameConstants.BlockTitle2))
              .ForMember(x => x.Image, m => m.MapFrom(c => c.Fields.GetValue<Guid>(BlockFieldNameConstants.BlockImage).MapTo<ImageModel>()))
              ;
        }
    }
}

