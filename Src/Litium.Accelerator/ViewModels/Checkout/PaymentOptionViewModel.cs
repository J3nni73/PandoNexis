using Litium.Accelerator.Builders;
using Litium.Sales;
using Litium.Sales.Payments;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Litium.Accelerator.ViewModels.Checkout
{
    public class PaymentOptionViewModel : IViewModel
    {
        public ProviderOptionIdentifier Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string FormattedPrice { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentIntegrationType IntegrationType { get; set; }
    }
}
