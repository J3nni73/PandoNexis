
using Litium.Owin.InversionOfControl;
using Litium.Products;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.SpecialFields
{
    [Plugin("CustomerNr")]
    public class PriceListCustomerNrField : GridSpecialFieldDataBase
    {
        public PriceListCustomerNrField()
        {

        }
        public override string GetValue(FieldConfigurationField field, Variant variant)
        {
            return field.Value.ToString();
        }
    }
}
