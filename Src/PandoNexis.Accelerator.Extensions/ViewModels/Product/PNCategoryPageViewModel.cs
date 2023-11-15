using AutoMapper;
using Litium.Accelerator.ViewModels.Product;
using Litium.Runtime.AutoMapper;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Product
{
    public class PNCategoryPageViewModel : CategoryPageViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL

        //PandoNexis GtmEnhancedEcom begin"

        public string GtmImpressionTracking { get; set; }
        public List<PNProductItemViewModel> PNProducts { get; set; }
        //PandoNexis GtmEnhancedEcom end"

        //PandoNexis: END MODEL


        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CategoryPageViewModel, PNCategoryPageViewModel>()
                .ForMember(x => x.PNProducts, m => m.MapFrom(x => x.Products));
        }

    }
}
