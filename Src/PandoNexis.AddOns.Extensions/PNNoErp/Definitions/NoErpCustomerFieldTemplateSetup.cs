using Litium.Accelerator.Constants;
using Litium.Customers;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Definitions
{
    internal class NoErpCustomerFieldTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>()
            {

                GetOrganizationField(CustomerTemplateIdConstants.OrganizationTemplate, "General", SystemFieldDefinitionConstants.NameInvariantCulture),
                GetOrganizationField(CustomerTemplateIdConstants.OrganizationTemplate, "General", SystemFieldDefinitionConstants.Description),
                GetOrganizationField(CustomerTemplateIdConstants.OrganizationTemplate, "General", "LegalRegistrationNumber"),
                GetOrganizationField(CustomerTemplateIdConstants.OrganizationTemplate, NoErpOrderAdminConstants.NoErp ,NoErpOrderAdminConstants.Authorization),
                GetOrganizationField(CustomerTemplateIdConstants.OrganizationTemplate, NoErpOrderAdminConstants.NoErp ,NoErpOrderAdminConstants.BaseUrl)
            };


            return templateChanges;
        }


        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new OrganizationFieldTemplate(CustomerTemplateIdConstants.OrganizationTemplate)
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

