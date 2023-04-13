using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNNoErp.Objects;
using PandoNexis.AddOns.Extensions.PNNoErp.Services.DataLayerServices;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Services
{
    [Service(ServiceType = typeof(NoErpOrderService))]
    public class NoErpOrderService
    {
        private readonly LitiumOrderService _litiumOrderService;
        public NoErpOrderService(LitiumOrderService litiumOrderService)
        {
            _litiumOrderService = litiumOrderService;
        }

        public List<Order> GetOrders()
        {
            var orders = _litiumOrderService.GetOrders().Result;

            return orders;
        }



        public Order CreateShippment(Guid orderSystemId)
        {
            var order = _litiumOrderService.CreateShippment(orderSystemId).Result;
            return order;
        }
        public Order NotifyOrderExported(Guid orderSystemId)
        {
            var order = _litiumOrderService.NotifyOrderExported(orderSystemId).Result;
            return order;

        }
        public Order FinalizeOrder(Guid orderSystemId)
        {
            var order = _litiumOrderService.FinalizeOrder(orderSystemId).Result;
            return order;

        }
        public Order NotifyShipmentDelivered(Guid orderSystemId)
        {
            var order = _litiumOrderService.NotifyShipmentDelivered(orderSystemId).Result;
            return order;

        }
        //private string GetTags(Guid orderSystemId)
        //{
        //    var order = _litiumOrderService.GetTags(orderSystemId).Result;
        //    return order;
        //}

        //private async Task<string> GetOrderState(Guid orderSystemId)
        //{

        //}
        public Order GetOrder(Guid orderSystemId)
        {
            var order = _litiumOrderService.GetOrder(orderSystemId).Result;
            return order;
        }

        private Order GetOrder(string orderId)
        {
            var order = _litiumOrderService.GetOrder(orderId).Result;
            return order;
        }
    }
}
