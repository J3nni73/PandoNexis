using AutoMapper;
using Litium.Accelerator.ViewModels.Checkout;
using Litium.Runtime.AutoMapper;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Checkout
{
    public class PNCheckoutViewModel : CheckoutViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL

        //PandoNexis GtmEnhancedEcom begin"
        public string GtmCheckoutTracking { get; set; }
        //PandoNexis GtmEnhancedEcom end"

        //PandoNexis: END MODEL

        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CheckoutViewModel, PNCheckoutViewModel>();
        }
    }
}
