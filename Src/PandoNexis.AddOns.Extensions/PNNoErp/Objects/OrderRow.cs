namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class OrderRow
    {
        //public string AdditionalInfo { get; set; }
        public string ArticleNumber { get; set; }
        public string Descriptions { get; set; }
        public string Id { get; set; }
        public double Quantity { get; set; }
        public double TotalExcludingVat { get; set; }
        public double TotalIncludingVat { get; set; }
        public double TotalVat { get; set; }
        public string Type { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double UnitPriceExcludingVat { get; set; }
        public double UnitPriceIncludingVat { get; set; }
        public double VatRate { get; set; }
    }
}
