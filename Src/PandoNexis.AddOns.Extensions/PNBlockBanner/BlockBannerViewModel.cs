using AutoMapper;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Blocks;
using Litium.Accelerator.Builders;
using PandoNexis.AddOns.Extensions.Block.Constants;
using Litium.Accelerator.Extensions;
using PandoNexis.Accelerator.Extensions.ViewModels;
using PandoNexis.AddOns.Extensions.Block.BlockBanner;

namespace PandoNexis.AddOns.Extensions.Block.BlockBanner
{
    public class BlockBannerViewModel : IViewModel, IAutoMapperConfiguration
    {

        public List<BlockBannerItemViewModel> Items { get; set; } = new List<BlockBannerItemViewModel>();
       


        [Litium.Owin.UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BlockModel, BlockBannerViewModel>()

             ;
        }
    }
}

