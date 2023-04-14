namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class PromotionInfo
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string DiscountType { get; set; }
        //public string AdditionalInfo { get; set; }
        public List<string> SourceOrderRowIds { get; set; }
        public string ResultOrdreRowId { get; set; }

    }
}
