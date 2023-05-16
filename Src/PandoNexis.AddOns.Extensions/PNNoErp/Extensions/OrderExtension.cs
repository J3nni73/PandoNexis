using Litium.FieldFramework;
using Litium.Web;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;
using PandoNexis.AddOns.Extensions.PNNoErp.Objects;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Extensions
{
    public static class OrderExtension
    {
        public static GenericDataField OrderIdAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpOrderFieldsConstants.OrderNumber;
            result.FieldName = "addons.noerp.headertexts.ordernumber".AsWebsiteText(website);
            result.FieldType = "string";
            result.FieldValue = order.Id;

            return result;
        }

        public static GenericDataField OrderDateAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpOrderFieldsConstants.OrderDate;
            result.FieldName = "addons.noerp.headertexts.orderdate".AsWebsiteText(website);
            result.FieldType = "string";
            result.FieldValue = order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss");

            return result;
        }
        public static GenericDataField OrderRowsCountAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpOrderFieldsConstants.OrderRowsCount;
            result.FieldName = "addons.noerp.headertexts.orderrowsamount".AsWebsiteText(website);
            result.FieldType = "int";
            result.FieldValue = order.Rows.Count().ToString();

            return result;
        }


        public static GenericDataField OrderTotalAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpOrderFieldsConstants.OrderTotal;
            result.FieldName = "addons.noerp.headertexts.ordertotal".AsWebsiteText(website);
            result.FieldType = "decimal";
            result.FieldValue = order.GrandTotal.ToString("##.00");

            return result;
        }

        public static GenericDataField OrderStateAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpOrderFieldsConstants.OrderState;
            result.FieldName = "addons.noerp.headertexts.orderstate".AsWebsiteText(website);
            result.FieldType = "string";
            result.FieldValue = order.OrderState;

            return result;
        }
        public static GenericDataField PaymentStateAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpPaymentFieldConstants.PaymentState;
            result.FieldName = "addons.noerp.headertexts.paymentstate".AsWebsiteText(website);
            result.FieldType = "string";
            result.FieldValue = order.PaymentState;

            return result;
        }
        public static GenericDataField ShipmentStateAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpShipmentFieldConstants.ShipmentState;
            result.FieldName = "addons.noerp.headertexts.shipmentstate".AsWebsiteText(website);
            result.FieldType = "string";
            result.FieldValue = order.ShipmentState;

            return result;
        }

        public static GenericDataField CustomerAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpOrderFieldsConstants.CustomerInfo;
            result.FieldName = "addons.noerp.headertexts.customerinfo".AsWebsiteText(website);
            result.FieldType = "string";


            result.FieldValue = order.CustomerInfo.OrganizationCustomerNumber ?? order.CustomerInfo.CustomerNumber;
            result.FieldValue += ": " + order.CustomerInfo.FirstName;
            result.FieldValue += " " + order.CustomerInfo.LastName;
            return result;

        }


        //Buttons
        public static GenericDataField NotifyOrderExportedAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpButtonConstants.NotifyOrderExported;
            result.FieldName = "addons.noerp.headertexts.acceptorder".AsWebsiteText(website);
            result.FieldType = "button";
            result.Settings.ButtonText = order.NotifyOrderExported ? "addons.noerp.headertexts.acceptorder".AsWebsiteText(website) : "";
            result.Settings.HideButton = !order.NotifyOrderExported;

            return result;
        }

        public static GenericDataField CreateShipmentAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpButtonConstants.CreateShipment;
            result.FieldName = "addons.noerp.headertexts.createshipment".AsWebsiteText(website);
            result.FieldType = "button";
            result.Settings.ButtonText = order.CreateShipment ? "addons.noerp.headertexts.createshipment".AsWebsiteText(website) : "";
            result.Settings.HideButton = !order.CreateShipment;

            return result;
        }


        public static GenericDataField NotifyShipmentDeliveredAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpButtonConstants.NotifyShipmentDelivered;
            result.FieldName = "addons.noerp.headertexts.notifyshippmentdelivered".AsWebsiteText(website);
            result.FieldType = "button";
            result.Settings.ButtonText = order.NotifyShipmentDelivered ? "addons.noerp.headertexts.notifyshippmentdelivered".AsWebsiteText(website) : "";
            result.Settings.HideButton = !order.NotifyShipmentDelivered;

            return result;
        }


        public static GenericDataField FinalizeOrderAsGenericField(this Order order, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpButtonConstants.FinalizeOrder;
            result.FieldName = "addons.noerp.headertexts.finalizeorder".AsWebsiteText(website);
            result.FieldType = "button";
            result.Settings.ButtonText = order.FinalizeOrder ? "addons.noerp.headertexts.finalizeorder".AsWebsiteText(website) : "";
            result.Settings.HideButton = !order.FinalizeOrder;

            return result;
        }
        public static GenericDataField ViewOrderAsGenericField(this Order order, Guid pageSystemId, Website website)
        {
            var result = new GenericDataField();

            result.EntitySystemId = order.SystemID.ToString();
            result.FieldId = NoErpButtonConstants.ViewOrder;
            result.FieldName = "addons.noerp.headertexts.vieworder".AsWebsiteText(website);
            result.FieldType = "button";
            result.Settings.ButtonText = "addons.noerp.headertexts.vieworder".AsWebsiteText(website);
            
            result.Settings.ButtonOpenInModal = true;

            var buttonLinksContainer = website.Fields.GetValue<IList<MultiFieldItem>>(NoErpOrderAdminConstants.NoErpButtonLinks);
            if (buttonLinksContainer != null)
            {
                foreach (var field in buttonLinksContainer)
                {
                    if (field.Fields.GetValue<string>(NoErpOrderAdminConstants.NoErpButtonNames) == NoErpButtonConstants.ViewOrder)
                    {
                        var test = field;
                        result.Settings.ModalPageSystemId = field.Fields.GetValue<Guid>(DataViewFieldNameConstants.ButtonPagePointer);
                        
                    }
                }
            }

            result.Settings.HideButton = result.Settings.ModalPageSystemId == Guid.Empty;
            return result;
        }
    }
}
