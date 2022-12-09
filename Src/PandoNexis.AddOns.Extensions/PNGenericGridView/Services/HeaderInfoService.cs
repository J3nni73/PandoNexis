using Litium.Runtime.DependencyInjection;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Services
{
    [Service(ServiceType = typeof(HeaderInfoService), Lifetime = DependencyLifetime.Singleton)]
    public class HeaderInfoService
    {


        public async Task<string> GetHeaderInfo(string type)
        {
            //var items = JsonConvert.DeserializeObject<JObject>(type);
            var returnString = string.Empty;
            switch (type)
            {
                case "QuickBuy":
                case "Checkout":


                    returnString += " <dl> <dt> Quantity </dt> <dd>10</dd> </dl> ";

                    break;

            }
            return returnString;
        }
    }
}
