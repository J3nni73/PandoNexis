using Litium.Accelerator.Extensions;

using Litium.Owin.InversionOfControl;
using Litium.Products;
using System.Globalization;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.SpecialFields
{
    [Plugin("ArticleName")]
    public class ArticleNameField : GridSpecialFieldDataBase
    {
        public override string GetValue(FieldConfigurationField field, Variant variant)
        {
            return variant.Fields.GetName(CultureInfo.CurrentCulture.Name);
        }
    }
}
