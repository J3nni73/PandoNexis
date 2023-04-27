using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;
using PandoNexis.AddOns.Extensions.PNNoErp.Extensions;
using PandoNexis.AddOns.Extensions.PNNoErp.Objects;
using PandoNexis.AddOns.Extensions.PNNoErp.Services;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Processors
{
    [Service(Name = "OrderView")]
    public class OrderViewProcessor : NoErpProcessorBase
    {
        private readonly NoErpOrderService _noErpOrderService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly PersonStorage _personStorage;
        private Guid _viewOrderSystemId;

        public OrderViewProcessor(PersonStorage personStorage,
                                    NoErpOrderService noErpOrderService,
                                    RequestModelAccessor requestModelAccessor, 
                                    GenericDataViewService genericDataViewService) : base(personStorage, genericDataViewService)
        {
            _noErpOrderService = noErpOrderService;
            _requestModelAccessor = requestModelAccessor;
            _personStorage = personStorage; 
        }

        public override Task<object> GetDataForm(string data)
        {
            throw new NotImplementedException();
        }

        public async override Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            var view = new GenericDataView();
            if (_personStorage?.CurrentSelectedOrganization == null) return view;


            var orders = _noErpOrderService.GetOrders();

            if (orders == null) { return view; }
            view.Settings = GetDataViewSettings(pageSystemId);

            foreach (var order in orders)
            {
                GenericDataContainer container = BuildOrderRow(order);
                view.DataContainers.Add(container);
            }


            return view;
        }

        private GenericDataContainer BuildOrderRow(Order order)
        {

            var website = _requestModelAccessor.RequestModel.WebsiteModel.Website;

            var container = new GenericDataContainer();
            var field = new GenericDataField();

            container.Fields.Add(order.OrderIdAsGenericField(website));
            container.Fields.Add(order.OrderDateAsGenericField(website));
            container.Fields.Add(order.OrderRowsCountAsGenericField(website));
            container.Fields.Add(order.ViewOrderAsGenericField(_viewOrderSystemId, website));
            container.Fields.Add(order.OrderTotalAsGenericField(website));
            container.Fields.Add(order.CustomerAsGenericField(website));
            container.Fields.Add(order.OrderStateAsGenericField(website));
            container.Fields.Add(order.ShipmentStateAsGenericField(website));
            container.Fields.Add(order.PaymentStateAsGenericField(website));

            //Buttons
            container.Fields.Add(order.NotifyOrderExportedAsGenericField(website));
            container.Fields.Add(order.CreateShipmentAsGenericField(website));
            container.Fields.Add(order.NotifyShipmentDeliveredAsGenericField(website));
            container.Fields.Add(order.FinalizeOrderAsGenericField(website));

            return container;
        }

        public override Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }

        public override Task<object> HandleFormData(string data)
        {
            throw new NotImplementedException();
        }

        public override Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            throw new NotImplementedException();
        }

        public async override Task<GenericDataContainer> ButtonClick( GenericDataField fieldData)
        {

            if (Guid.TryParse(fieldData.EntitySystemId, out Guid orderSystemId))
            {
                var container = new GenericDataContainer();
                Order order = null;

                switch (fieldData.FieldID)
                {
                    case NoErpButtonConstants.NotifyOrderExported:
                        order = _noErpOrderService.NotifyOrderExported(orderSystemId);
                        break;
                    case NoErpButtonConstants.CreateShipment:
                        order = _noErpOrderService.CreateShippment(orderSystemId);
                        break;
                    case NoErpButtonConstants.NotifyShipmentDelivered:
                        order = _noErpOrderService.NotifyShipmentDelivered(orderSystemId);
                        break;
                    case NoErpButtonConstants.FinalizeOrder:
                        order = _noErpOrderService.FinalizeOrder(orderSystemId);
                        break;

                }
                if (order == null)
                {
                    order = _noErpOrderService.GetOrder(orderSystemId);
                }
                container = BuildOrderRow(order);
                return container;
            }
            return null;

        }
    }
}
