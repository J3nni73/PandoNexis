using Litium.Accelerator.ViewModels.Block;
using AutoMapper;
using Litium.Runtime.AutoMapper;
using PandoNexis.Accelerator.Extensions.ViewModels.Product;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Block
{
    public class PNProductBlockViewModel : ProductBlockViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL

        //PandoNexis GtmEnhancedEcom begin"
        public List<PNProductItemViewModel> PNProducts { get; set; }
        public string GtmImpressionTracking { get; set; }
        //PandoNexis GtmEnhancedEcom end"

        //PandoNexis: END MODEL

        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProductBlockViewModel, PNProductBlockViewModel>()
                .ForMember(x => x.PNProducts, m => m.MapFrom(x => x.Products));
        }
    }
}
