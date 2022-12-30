
using DocumentFormat.OpenXml.Wordprocessing;
using Litium.Owin.InversionOfControl;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.SpecialFields
{
    [Plugin("ArticleNumber")]
    
    public class ArticleNumberField : GridSpecialFieldDataBase
    {
        public override string GetValue(FieldConfigurationField field, Variant variant)
        {
            return variant.Id;
        }
    }
}
