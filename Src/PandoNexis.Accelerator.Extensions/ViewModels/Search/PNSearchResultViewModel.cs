using AutoMapper;
using Litium.Runtime.AutoMapper;
using Litium.Accelerator.ViewModels.Search;
using Litium.Accelerator.ViewModels.Product;
using PandoNexis.Accelerator.Extensions.ViewModels.Product;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Search
{
    /// <summary>
    /// Search result view model
    /// </summary>
    public class PNSearchResultViewModel : SearchResultViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL

        //PandoNexis GtmEnhancedEcom begin"
        public string GtmImpressionTracking { get; set; }
        public IEnumerable<PNProductItemViewModel> PNProducts { get; set; } = Enumerable.Empty<PNProductItemViewModel>();
        //PandoNexis GtmEnhancedEcom end"

        //PandoNexis: END MODEL


        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<SearchResultViewModel, PNSearchResultViewModel>()
                .ForMember(x => x.PNProducts, m => m.MapFrom(x => x.Products));
        }
    }
}
