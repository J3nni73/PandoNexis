using Newtonsoft.Json;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class Shipment
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("orderId")]
        public string OrderId { get; set; }
        [JsonProperty("shippingMethod")]
        public string ShippingMethod { get; set; }

        //[JsonProperty("additionalInfo")]
        //public string AdditionalInfo { get; set; }

        [JsonProperty("address")]
        public Address Address{ get; set; }

        [JsonProperty("rows")]
        public List<ShipmentRow> Rows { get; set; }

        [JsonProperty("deliveryCarrierId")]
        public string DeliveryCarrierId { get; set; }

        [JsonProperty("trackingReference")]
        public string TrackingReference { get; set; }

        [JsonProperty("trackingUrl")]
        public string TrackingUrl { get; set; }

        [JsonProperty("returnSlipDeliveryCarrierId")]
        public string ReturnSlipDeliveryCarrierId { get; set; }

        [JsonProperty("returnSlipTrackingReference")]
        public string ReturnSlipTrackingReference { get; set; }

        [JsonProperty("returnSlipUrl")]
        public string ReturnSlipUrl { get; set; }
    }
}
