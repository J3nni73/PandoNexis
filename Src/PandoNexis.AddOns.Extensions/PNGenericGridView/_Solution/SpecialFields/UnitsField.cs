using Litium.Owin.InversionOfControl;
using Litium.Products;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.SpecialFields
{
    [Plugin("Units")]
    public class UnitsField : GridSpecialFieldDataBase
    {
        public override string GetValue(FieldConfigurationField field, Variant variant)
        {
            return field.Value != null ? ((decimal)field.Value).ToString("0.##") : null;
        }

        public override string GetGridViewFieldType()
        {
            return "decimal";
        }
    }
}
