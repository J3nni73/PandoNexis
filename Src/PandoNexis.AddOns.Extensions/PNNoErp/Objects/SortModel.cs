using Newtonsoft.Json;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class SortModel
    {
        [JsonProperty("fieldId")]
        public string FieldId { get; set; }
        [JsonProperty("direction")]
        public string Direction { get; set; }
    }
}
