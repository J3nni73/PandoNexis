using AutoMapper;
using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.AutoMapper;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Framework
{
    public class PNCartViewModel : CartViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL
        
        //PandoNexis GtmEnhancedEcom begin"
        public IList<PNOrderRowViewModel> PNOrderRows { get; set; }
        public IList<PNOrderRowViewModel> PNDiscountRows { get; set; }
        //PandoNexis GtmEnhancedEcom end"

        //PandoNexis: END MODEL

        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CartViewModel, PNCartViewModel>()
                .ForMember(x => x.PNOrderRows, m => m.MapFrom(x => x.OrderRows))
                .ForMember(x => x.PNDiscountRows, m => m.MapFrom(x => x.DiscountRows));
        }
    }
}
