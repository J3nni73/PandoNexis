using AutoMapper;
using Litium.Accelerator.ViewModels.Search;
using Litium.Runtime.AutoMapper;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Search
{
    public class PNQuickSearchResultViewModel : QuickSearchResultViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL

        //PandoNexis GtmEnhancedEcom begin"
        public Object GtmImpressionTracking { get; set; }
        //PandoNexis GtmEnhancedEcom end"

        //PandoNexis: END MODEL


        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<QuickSearchResultViewModel, PNQuickSearchResultViewModel>();
        }
    }
}
