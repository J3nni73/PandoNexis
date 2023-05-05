using Litium.Accelerator.Definitions;
using Litium.Customers;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Constants;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using AcceleratorCustomerTemplate = Litium.Accelerator.Constants;
namespace PandoNexis.Accelerator.Extensions.Definitions.Customers
{
    internal class PersonB2BFieldTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>()
            {

                GetPersonField(AcceleratorCustomerTemplate.CustomerTemplateIdConstants.B2BPersonTemplate, "General", SystemFieldDefinitionConstants.FirstName),
                GetPersonField(AcceleratorCustomerTemplate.CustomerTemplateIdConstants.B2BPersonTemplate, "General", SystemFieldDefinitionConstants.LastName),
                GetPersonField(AcceleratorCustomerTemplate.CustomerTemplateIdConstants.B2BPersonTemplate, "General", SystemFieldDefinitionConstants.Email),
                GetPersonField(AcceleratorCustomerTemplate.CustomerTemplateIdConstants.B2BPersonTemplate, "General", SystemFieldDefinitionConstants.Phone),

            };


            return templateChanges;
        }


        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new PersonFieldTemplate(AcceleratorCustomerTemplate.CustomerTemplateIdConstants.B2BPersonTemplate)
            {
                FieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,
                        },
                    },
            };
            return template;
        }
    }
}

