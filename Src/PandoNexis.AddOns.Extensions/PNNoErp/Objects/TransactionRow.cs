namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class TransactionRow
    {
        public string OrderRowId { get; set; }
        public string Type { get; set; }
        public string Descriptions { get; set; }
        public double UnitPriceIncludingVat { get; set; }
        public double UnitPriceExcludingVat { get; set; }
        public double Quantity { get; set; }
        public double VatRate { get; set; }
        public double TotalIncludingVat { get; set; }
        public double TotalExcludingVat { get; set; }
        public double TotalVat { get; set; }
        //public string AdditionalInfo { get; set; }
    }
}
