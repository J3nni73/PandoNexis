using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;

namespace PandoNexis.AddOns.Extensions.PNContactForm.Definitions
{
    internal class PageFieldDefinitionSetup : Litium.Accelerator.Definitions.FieldDefinitionSetup
    {
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
                new FieldDefinition<CustomerArea>(ContactFormConstants.ContactFormCompany, SystemFieldTypeConstants.Text),
                new FieldDefinition<CustomerArea>(ContactFormConstants.ContactFormMessage, SystemFieldTypeConstants.MultirowText),
                new FieldDefinition<CustomerArea>(ContactFormConstants.AddedByContactForm, SystemFieldTypeConstants.Boolean)
                {
                    CanBeGridColumn = true,
                    CanBeGridFilter = true,
                }
            };
            return fields;
        }
    }
}
