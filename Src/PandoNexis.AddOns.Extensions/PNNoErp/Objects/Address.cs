using Newtonsoft.Json;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Objects
{
    public class Address
    {

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("careOf")]
        public string CareOf { get; set; }
        [JsonProperty("addrss1")]
        public string Addrss1 { get; set; }
        [JsonProperty("address2")]
        public string Address2 { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("fax")]
        public string Fax { get; set; }
        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("zip")]
        public string Zip { get; set; }
        [JsonProperty("houseExtension")]
        public string HouseExtension { get; set; }
        [JsonProperty("houseNumber")]
        public string HouseNumber { get; set; }
        [JsonProperty("organizationName")]
        public string OrganizationName { get; set; }

    }
}
