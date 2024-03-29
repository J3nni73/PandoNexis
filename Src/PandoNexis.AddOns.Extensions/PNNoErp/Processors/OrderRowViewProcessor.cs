﻿using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using PandoNexis.AddOns.Extensions.PNNoErp.Objects;
using PandoNexis.AddOns.Extensions.PNNoErp.Services;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Processors
{
    [Service(Name = "OrderRowView")]
    public class OrderRowViewProcessor : NoErpProcessorBase
    {
        private readonly NoErpOrderService _noErpOrderService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly PersonStorage _personStorage;
        

        public OrderRowViewProcessor(PersonStorage personStorage,
                                    NoErpOrderService noErpOrderService,
                                    RequestModelAccessor requestModelAccessor,
                                    GenericDataViewService genericDataViewService) : base(personStorage, genericDataViewService)
        {
            _noErpOrderService = noErpOrderService;
            _requestModelAccessor = requestModelAccessor;
            _personStorage = personStorage;
        }

        public async override Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            var view = new GenericDataView();
            if (_personStorage?.CurrentSelectedOrganization == null) return view;

            var orderid = Guid.Parse(data.Replace("?entitySystemId=", ""));

            var order = _noErpOrderService.GetOrder(orderid);

            if (order == null) { return view; }

            view.Settings = GetDataViewSettings(pageSystemId);

            foreach (var orderRow in order.Rows)
            {
                GenericDataContainer container = BuildOrderRow(orderRow);
                view.DataContainers.Add(container);
            }

            return view;
        }

        private GenericDataContainer BuildOrderRow(OrderRow orderRow)
        {

            var website = _requestModelAccessor.RequestModel.WebsiteModel.Website;

            var container = new GenericDataContainer();
            var field = new GenericDataField();

            field.FieldName = "ArticleNumber";
            field.EntitySystemId = orderRow.ArticleNumber;
            field.FieldId = "articlenumber";
            field.FieldType = "string";
            field.FieldValue = orderRow.ArticleNumber;
            container.Fields.Add(field);

            field = new GenericDataField();
            field.FieldName = "Description";
            field.EntitySystemId = orderRow.ArticleNumber;
            field.FieldId = "description";
            field.FieldType = "string";
            field.FieldValue = orderRow.Descriptions;
            container.Fields.Add(field);


            field = new GenericDataField();
            field.FieldName = "Quantity";
            field.EntitySystemId = orderRow.Id;
            field.FieldId = "orderqantity";
            field.FieldType = "string";
            field.FieldValue = orderRow.Quantity.ToString();
            container.Fields.Add(field);


            return container;
        }

        public override Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }
        public override Task<GenericDataContainer> UpdateField(GenericDataField fieldData)
        {
            throw new NotImplementedException();
        }

        public async override Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            var container = new GenericDataContainer();

            //Order order = null;


            //switch (fieldData.FieldId)
            //{
            //    case NoErpOrderStateConstants.NotifiyOrderExported:
            //        order = await _noErpOrderService.NotifyOrderExported(Guid.Parse(fieldData.EntitySystemId));
            //        break;
            //    case NoErpOrderStateConstants.CreateShippment:
            //        order = await _noErpOrderService.CreateShippment(Guid.Parse(fieldData.EntitySystemId));
            //        break;
            //    case NoErpOrderStateConstants.NotifyShipmentDelivered:
            //        order = await _noErpOrderService.NotifyShipmentDelivered(Guid.Parse(fieldData.EntitySystemId));
            //        break;
            //    case NoErpOrderStateConstants.FinalizeOrder:
            //        order = await _noErpOrderService.FinalizeOrder(Guid.Parse(fieldData.EntitySystemId));
            //        break;

            //}
            //if (order != null)
            //{
            //    container = BuildOrderRow(order);
            //    return container;
            //}
            return container;
        }
    }
}
