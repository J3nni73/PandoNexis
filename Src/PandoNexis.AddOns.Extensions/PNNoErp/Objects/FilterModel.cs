using Newtonsoft.Json;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class FilterModel
    {
        [JsonProperty("fieldId")]
        public string FieldId { get; set; }
        [JsonProperty("direction")]
        public string Direction { get; set; }
    }
}
