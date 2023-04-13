using Litium.Runtime.DependencyInjection;
using Newtonsoft.Json;
using PandoNexis.AddOns.Extensions.PNNoErp.Objects;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Services.DataLayerServices
{
    [Service(ServiceType = typeof(LitiumOrderService))]
    public class LitiumOrderService
    {
        private readonly ConnectToLitiumService _connectToLitiumService;
        public LitiumOrderService(ConnectToLitiumService connectToLitiumService)
        {
            _connectToLitiumService = connectToLitiumService;
        }

        public async Task<List<Order>?> GetOrders()
        {

            var method = $"/Litium/api/admin/sales/salesOrders/search";
            var searchModel = new SearchModel();

            var result = await _connectToLitiumService.PostData(method, JsonConvert.SerializeObject(searchModel), new Dictionary<string, string>());
            var convertedResult = JsonConvert.DeserializeObject<SearchResultSalesOrder>(result);
            if (convertedResult?.items == null) return null;
            var orders = convertedResult.items;

            var orderResult = new List<Order>();
            foreach (var order in orders)
            {
                var state = await GetOrderState(order.SystemID);
                var newOrder = GetOrder(order.Id).Result;
                newOrder.SystemID = order.SystemID;
                newOrder.OrderDate = order.OrderDate;
                newOrder.OrderState = state;
                var tags = await GetTags(order.SystemID);


                orderResult.Add(newOrder);
            }


            return orderResult.OrderByDescending(i => i.OrderDate).ToList();
        }



        public async Task<Order> CreateShippment(Guid orderSystemId)
        {

            var order = await GetOrder(orderSystemId);
            var shippinginfo = order.ShippingInfo.FirstOrDefault();

            var shippmentRows = new List<ShipmentRow>();
            foreach (var row in order.Rows)
            {
                var shipmentRow = new ShipmentRow();
                shipmentRow.ArticleNumber = row.ArticleNumber;
                shipmentRow.Quantity = row.Quantity;
                shipmentRow.OrderRowId = row.Id;
                shippmentRows.Add(shipmentRow);

            }
            var shippment = new Shipment()
            {
                Id = order.Id + "shippment",
                OrderId = order.Id,
                ShippingMethod = shippinginfo.shippingMethod,
                Address = shippinginfo.address,
                Rows = shippmentRows
            };


            var method = $"/Litium/api/connect/erp/orders/{order.Id}/shipments";

            var result = await _connectToLitiumService.PostData(method, JsonConvert.SerializeObject(shippment), new Dictionary<string, string>());
            var resultOrder = await GetOrder(orderSystemId);
            return resultOrder;
        }
        public async Task<Order> NotifyOrderExported(Guid orderSystemId)
        {
            var order = await GetOrder(orderSystemId);

            var method = $"/Litium/api/connect/erp/orders/{order.Id}/notify/exported";

            var result = await _connectToLitiumService.PostData(method, string.Empty, new Dictionary<string, string>());
            order = await GetOrder(orderSystemId);
            return order;

        }
        public async Task<Order> FinalizeOrder(Guid orderSystemId)
        {
            var order = await GetOrder(orderSystemId);
            var method = $"/Litium/api/connect/erp/orders/{order.Id}/action/finalize";

            var result = await _connectToLitiumService.PostData(method, string.Empty, new Dictionary<string, string>());
            order = await GetOrder(orderSystemId);
            return order;

        }
        public async Task<Order> NotifyShipmentDelivered(Guid orderSystemId)
        {
            var order = await GetOrder(orderSystemId);
            var method = $"/Litium/api/connect/erp/orders/{order.Id}/shipments/{order.Shipments.FirstOrDefault().Id}/notify/delivered";

            var result = await _connectToLitiumService.PostData(method, string.Empty, new Dictionary<string, string>());
            order = await GetOrder(orderSystemId);
            return order;
        }
        public async Task<string> GetTags(Guid orderSystemId)
        {
            var method = $"/Litium/api/admin/sales/salesOrders/{orderSystemId.ToString()}/tags";
            var result = await _connectToLitiumService.GetData(method, new Dictionary<string, string>());
            return result;
        }

        public async Task<string> GetOrderState(Guid orderSystemId)
        {
            var method = $"/Litium/api/admin/sales/salesOrders/{orderSystemId.ToString()}/stateTransition";
            var result = await _connectToLitiumService.GetData(method, new Dictionary<string, string>());
            return result;
        }
        public async Task<Order> GetOrder(Guid orderSystemId)
        {
            var orders = await GetOrders();

            var order = orders.FirstOrDefault(i => i.SystemID == orderSystemId);

            return order;

        }

        public async Task<Order> GetOrder(string orderId)
        {
            var method = $"/Litium/api/connect/erp/orders/{orderId}";
            var result = await _connectToLitiumService.GetData(method, new Dictionary<string, string>());

            var order = JsonConvert.DeserializeObject<Order>(result);
            return order;

        }


    }
}
