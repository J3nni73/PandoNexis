using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;

namespace PandoNexis.AddOns.Extensions.PNContactForm.Definitions
{
    internal class PageFieldDefinitionSetup : Litium.Accelerator.Definitions.FieldDefinitionSetup
    {
        public readonly FieldHelper _fieldHelper;

        public PageFieldDefinitionSetup(FieldHelper fieldHelper)
        {
            _fieldHelper = fieldHelper;
        }

        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>();

            fields.AddRange(ContactFormFields());

            return fields;
        }

        private IEnumerable<FieldDefinition> ContactFormFields()
        {
            var fields = new List<FieldDefinition>
            {
                _fieldHelper.GetCustomerFieldDefinition(ContactFormConstants.ContactFormCompany, SystemFieldTypeConstants.Text),
                _fieldHelper.GetCustomerFieldDefinition(ContactFormConstants.ContactFormMessage, SystemFieldTypeConstants.MultirowText),
                _fieldHelper.GetCustomerFieldDefinition(ContactFormConstants.AddedByContactForm, SystemFieldTypeConstants.Boolean, false, true, true),
                _fieldHelper.GetCustomerFieldDefinition(ContactFormConstants.ContactAccept, SystemFieldTypeConstants.Boolean, false, true, true),
            };
            return fields;
        }
    }
}
