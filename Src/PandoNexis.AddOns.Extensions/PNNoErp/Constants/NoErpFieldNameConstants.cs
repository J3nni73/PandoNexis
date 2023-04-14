namespace PandoNexis.AddOns.Extensions.PNNoErp.Constants
{
    public static class NoErpOrderFieldsConstants
    {
        public static string OrderNumber= "OrderNumber";
        public static string OrderDate = "OrderDate";
        public static string OrderTotal = "OrderTotal";
        public static string OrderState = "OrderState";
        public static string OrderRowsCount = "OrderRowsCount";
        public static string CustomerInfo = "CustomerInfo";
    }
    public static class NoErpShipmentFieldConstants
    {
        public const string ShipmentState = "ShipmentState";

    }
    public static class NoErpPaymentFieldConstants
    {
        public const string PaymentState = "PaymentState";
    }

    public static class NoErpButtonConstants
    {
        public const string NotifyOrderExported = "NotifyOrderExported";
        public const string CreateShipment = "CreateShipment";
        public const string NotifyShipmentDelivered = "NotifyShipmentDelivered";
        public const string FinalizeOrder = "FinalizeOrder";

        public const string ViewOrder = "ViewOrder";
    }
}
