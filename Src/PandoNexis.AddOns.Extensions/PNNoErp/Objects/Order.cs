namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class Order
    {
        public string Id { get; set; }
        public Guid SystemID { get; set; }
        public string OrderState { get; set; }
        public string CurrencyCode { get; set; }
        public double GrandTotal { get; set; }
        public DateTime OrderCreatedAt { get; set; }
        public DateTime OrderDate { get; set; }
        public string Origin { get; set; }
        public double TotalVat { get; set; }
        public string ChannelId { get; set; }
        public string WebSiteId { get; set; }
        public string MarketId { get; set; }
        public string CountryCode { get; set; }
        public string Type { get; set; }
        public string ClientIp { get; set; }
        public string OriginalSalesOrderId { get; set; }
        public List<Guid> SalesReturnOrderIds { get; set; }
        public List<Guid> RmaIds { get; set; }
        public string Comments { get; set; }
        public object AdditionalInfo { get; set; }
        public List<OrderRow> Rows { get; set; }
        public object VatSummary { get; set; }
        public List<PromotionInfo> Promotions { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Shipment> Shipments { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public Address billingAddress { get; set; }
        public List<ShippingInfo> ShippingInfo { get; set; }
        public List<Rma> Rmas { get; set; }

        public string PaymentState
        {
            get
            {   
                if (Payments?.FirstOrDefault()?.Transactions?.LastOrDefault(i=>i.Type== "capture") != null)
                {
                    return Payments.FirstOrDefault().Transactions.LastOrDefault(i => i.Type == "capture").Type + ": "+ Payments.FirstOrDefault().Transactions.LastOrDefault(i => i.Type == "capture").Status;
                }
                if (Payments?.FirstOrDefault()?.Transactions?.LastOrDefault(i => i.Type == "authorize") != null)
                {
                    return Payments.FirstOrDefault().Transactions.LastOrDefault(i => i.Type == "authorize").Type + ": " + Payments.FirstOrDefault().Transactions.LastOrDefault(i => i.Type == "authorize").Status;
                }
                if (Payments?.FirstOrDefault()?.Transactions?.LastOrDefault(i => i.Type == "init") != null)
                {
                    return Payments.FirstOrDefault().Transactions.LastOrDefault(i => i.Type == "init").Type + ": " + Payments.FirstOrDefault().Transactions.LastOrDefault(i => i.Type == "init").Status;
                }
                return string.Empty;
            }
        }
        public string ShipmentState
        {
            get
            {
                if(Shipments==null || !Shipments.Any())
                    return "No shipment created";
                if (Shipments.Any() && OrderState == Litium.Sales.OrderState.Processing)
                    return "Not delivered";
                if (OrderState == Litium.Sales.OrderState.Completed)
                    return "Delivered";
                return "";
            }
        }

        public bool NotifyOrderExported
        {
            get 
            {
                return OrderState == Litium.Sales.OrderState.Confirmed;
            }
        }
        public bool FinalizeOrder
        {
            get
            {
                return OrderState == Litium.Sales.OrderState.Processing && Shipments.Any();
            }
        }
        public bool CreateShipment
        {
            get
            {
                return OrderState == Litium.Sales.OrderState.Processing && (Shipments==null || !Shipments.Any());
            }
        }
        public bool NotifyShipmentDelivered
        {
            get
            {
                return OrderState == Litium.Sales.OrderState.Processing && Shipments.Any();
            }
        }
        public bool UpdateShipmentTracking
        {
            get
            {
                return OrderState == Litium.Sales.OrderState.Processing && Shipments.Any();
            }
        }
    }
}
