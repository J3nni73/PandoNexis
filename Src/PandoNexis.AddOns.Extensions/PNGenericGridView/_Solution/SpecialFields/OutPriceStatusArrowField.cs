
using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using Litium.Accelerator.Routing;
using Litium.Owin.InversionOfControl;
using Litium.Products;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.SpecialFields
{
    [Plugin("OutPriceStatusArrow")]
    public class OutPriceStatusArrowField : GridSpecialFieldDataBase
    {

        private readonly RequestModelAccessor _requestModelAccessor;

        public OutPriceStatusArrowField(RequestModelAccessor requestModelAccessor)
        {
            _requestModelAccessor = requestModelAccessor;
        }


        public override GenericGridViewFieldSettings GetSettings(FieldConfigurationField field)
        {
            var fieldSettings = new GenericGridViewFieldSettings();
            fieldSettings.ReadOnly = true;
            fieldSettings.FieldMessage = field.FieldMessage;

            return fieldSettings;
        }


        public override string GetValue(FieldConfigurationField field, Variant variant)
        {
            return string.Empty;
        }
    }
}
