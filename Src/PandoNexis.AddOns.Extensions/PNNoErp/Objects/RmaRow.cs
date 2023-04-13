namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class RmaRow
    {
        public string Id { get; set; }
        public string CustomerComments { get; set; }
        public string ArticleNumber { get; set; }
        public double QuantityReturned { get; set; }
        public double QuantityReceived { get; set; }
        public string UnitOfMeasurementId { get; set; }
        public string AdminComments { get; set; }
        //public string AdditionalInfo { get; set; }
        public object ReturnReason { get; set; }
        public string InventoryDecision { get; set; }
    }
}
