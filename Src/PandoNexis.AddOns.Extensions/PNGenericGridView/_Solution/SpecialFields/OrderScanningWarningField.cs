
using Litium.Owin.InversionOfControl;
using Litium.Products;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.SpecialFields
{
    [Plugin("OrderScanningWarning")]
    public class OrderScanningWarningField : GridSpecialFieldDataBase
    {
        public override string GetValue(FieldConfigurationField field, Variant variant)
        {
            return field.Value != null ? field.Value.ToString() : null;
        }

        public override string GetGridViewFieldType()
        {
            return "html";
        }
    }
}
