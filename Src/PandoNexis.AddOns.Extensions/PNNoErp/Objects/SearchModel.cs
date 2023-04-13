using Newtonsoft.Json;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class SearchModel
    {
        [JsonProperty("take")]
        public int Take { get; set; } = 20;
        [JsonProperty("skip")]
        public int Skip { get; set; } = 0;
        [JsonProperty("filters")]
        public NotifyFilters Filters { get; set; }
    }
}
