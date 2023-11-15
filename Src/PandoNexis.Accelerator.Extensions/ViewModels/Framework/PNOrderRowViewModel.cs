using AutoMapper;
using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.AutoMapper;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Framework
{
    public class PNOrderRowViewModel : OrderRowViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL

        //PandoNexis GtmEnhancedEcom begin"
        public string GtmTrackingData { get; set; }
        public string FbTrackingData { get; set; }
        public string SnapTrackingData { get; set; }
        public string PinTrackingData { get; set; }
        //PandoNexis GtmEnhancedEcom end"

        //PandoNexis: END MODEL

        public void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OrderRowViewModel, PNOrderRowViewModel>();
        }
    }
}
