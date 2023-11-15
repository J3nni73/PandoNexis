using AutoMapper;
using Litium.Accelerator.ViewModels.Order;
using Litium.Runtime.AutoMapper;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Order
{
    public class PNOrderConfirmationViewModel : OrderConfirmationViewModel, IAutoMapperConfiguration
    {
        public string GtmPurchaseTracking { get; set; } = string.Empty;

        public PNOrderDetailsViewModel Order { get; set; } = new PNOrderDetailsViewModel();
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OrderConfirmationViewModel, PNOrderConfirmationViewModel>()
                .ForMember(x => x.Order, m => m.MapFrom(x => x.Order));
        }
    }
}