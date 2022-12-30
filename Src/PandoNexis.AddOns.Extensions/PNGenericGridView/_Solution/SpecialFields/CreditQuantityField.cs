
using Litium.Owin.InversionOfControl;
using Litium.Products;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.SpecialFields
{
    [Plugin("CreditQuantity")]
    public class CreditQuantityField : GridSpecialFieldDataBase
    {
        public override string GetValue(FieldConfigurationField field, Variant variant)
        {
            return field.Value != null ? field.Value.ToString().Replace(",", ".") : null;
        }
        public override string GetGridViewFieldType()
        {
            return "decimal";
        }
    }
}
