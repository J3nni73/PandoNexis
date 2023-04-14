using Newtonsoft.Json;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class ShipmentRow
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("articleNumber")]
        public string ArticleNumber { get; set; }
        [JsonProperty("quantity")]
        public double Quantity { get; set; }
        //[JsonProperty("additionalInfo")]
        //public string AdditionalInfo { get; set; }
        [JsonProperty("orderRowId")]
        public string OrderRowId { get; set; }
    }
}
