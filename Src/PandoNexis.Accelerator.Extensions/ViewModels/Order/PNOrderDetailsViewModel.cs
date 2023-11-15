using AutoMapper;
using JetBrains.Annotations;
using Litium.Accelerator.ViewModels.Order;
using Litium.Runtime.AutoMapper;
using Litium.Web.Models.Products;
using Litium.Web.Models;
using PandoNexis.Accelerator.Extensions.ViewModel;

namespace PandoNexis.Accelerator.Extensions.ViewModels.Order
{
    public class PNOrderDetailsViewModel : OrderDetailsViewModel, IAutoMapperConfiguration
    {
        //PandoNexis: BEGIN MODEL

        //PandoNexis GtmEnhancedEcom begin"
        public string GtmTrackingData { get; set; }
        public string FbTrackingData { get; set; }
        public string SnapTrackingData { get; set; }
        public string PinTrackingData { get; set; }
        public IList<OrderRowItem> PNOrderRows { get; set; } = new List<OrderRowItem>();
        public CustomerInfoModel PNCustomerInfo { get; set; } = new CustomerInfoModel();

        public PNCookieConsentModel CookieConsent { get; set; }

        public class CustomerInfoModel
        {
            public string Email { get; set; }
            public string CustomerNumber { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address1 { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string Zip { get; set; }
        }

        public class OrderRowItem
        {
            public string Id { get; set; }
            public string Brand { get; set; }
            public string Name { get; set; }
            public string QuantityString { get; set; }
            public ProductPriceModel PriceInfo { get; set; }
            public string TotalPrice { get; set; }
            public Guid DeliveryId { get; set; }
            public LinkModel Link { get; set; }
            public bool IsFreeGift { get; set; }
        }
        //PandoNexis GtmEnhancedEcom end"

        //PandoNexis: END MODEL

        [UsedImplicitly]
        void IAutoMapperConfiguration.Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OrderDetailsViewModel, PNOrderDetailsViewModel>()
                .ForMember(x => x.PNOrderRows, m => m.MapFrom(x => x.OrderRows))
                .ForMember(x => x.PNCustomerInfo, m => m.MapFrom(x => x.CustomerInfo));
        }
    }
}
