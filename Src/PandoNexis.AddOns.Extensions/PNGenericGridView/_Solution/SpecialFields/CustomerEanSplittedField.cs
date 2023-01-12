
using Litium.Owin.InversionOfControl;
using Litium.Products;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.SpecialFields
{
    [Plugin("CustomerEanSplitted")]
    public class CustomerEanSplittedField : GridSpecialFieldDataBase
    {
        public override string GetValue(FieldConfigurationField field, Variant variant)
        {
            return field?.Value?.ToString()??string.Empty;
        }
        public override string GetGridViewFieldType()
        {
            return "string";
        }
    }
}
