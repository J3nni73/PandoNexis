using Litium.Owin.InversionOfControl;
using Litium.Products;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.SpecialFields
{
    [Plugin("TotalRowPrice")]
    public class TotalRowPriceField : GridSpecialFieldDataBase
    {
        public override string GetValue(FieldConfigurationField field, Variant variant)
        {
            return field.Value.ToString();
        }
        public override string GetGridViewFieldType()
        {
            return "decimal";
        }
    }
}
