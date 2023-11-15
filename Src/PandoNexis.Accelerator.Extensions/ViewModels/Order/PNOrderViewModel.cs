using AutoMapper;
using JetBrains.Annotations;
using Litium.Accelerator.ViewModels.Order;
using Litium.Runtime.AutoMapper;
using PandoNexis.Accelerator.Extensions.ViewModel;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Order
{
    public class PNOrderViewModel : OrderViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL

        //PandoNexis GtmEnhancedEcom begin"
        public string GtmTrackingData { get; set; }
        public string FbTrackingData { get; set; }
        public string SnapTrackingData { get; set; }
        public string PinTrackingData { get; set; }
        //PandoNexis GtmEnhancedEcom end"

        //PandoNexis: END MODEL

        public PNCookieConsentModel CookieConsent { get; set; }

        [UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OrderViewModel, PNOrderViewModel>();
        }
    }
}
