using Litium.Accelerator.Definitions;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Sales;
using PandoNexis.Accelerator.Extensions.Constants;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;

namespace PandoNexis.Accelerator.Extensions.Definitions.Customers
{
    internal class CustomersFieldTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetOrganizationField(CustomerTemplateIdConstants.SystemAdminUser,"General", SystemFieldDefinitionConstants.FirstName),
                GetOrganizationField(CustomerTemplateIdConstants.SystemAdminUser, "General", SystemFieldDefinitionConstants.LastName),
                GetOrganizationField(CustomerTemplateIdConstants.SystemAdminUser, "General", SystemFieldDefinitionConstants.Email),
                GetOrganizationField(CustomerTemplateIdConstants.SystemAdminUser, "General", SystemFieldDefinitionConstants.Phone),
                GetOrganizationField(CustomerTemplateIdConstants.SystemAdminUser, "General", "SocialSecurityNumber")
            };

            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var tempalate = new PersonFieldTemplate(CustomerTemplateIdConstants.SystemAdminUser)
                {
                FieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,
                            Fields =
                            {

                            }
                        }
                    }
            };
            return tempalate;
        }
    }
}
