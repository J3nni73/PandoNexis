using Litium.Accelerator.Definitions;
using Litium.Customers;
using Litium.FieldFramework;
using PandoNexis.AddOns.Extensions.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.Definitions.Customers
{
    internal class CustomersFieldTemplateSetup : FieldTemplateSetup
    {
        public override IEnumerable<FieldTemplate> GetTemplates()
        {
            var items = new FieldTemplate[]
            {
                new PersonFieldTemplate(CustomerTemplateIdConstants.SystemAdminUser)
                {
                    FieldGroups = new []
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,
                            Fields =
                            {
                                SystemFieldDefinitionConstants.FirstName,
                                SystemFieldDefinitionConstants.LastName,
                                SystemFieldDefinitionConstants.Email,
                                SystemFieldDefinitionConstants.Phone,
                                "SocialSecurityNumber"
                            }
                        }
                    }
                },
            };
            return items;
        }
    }
}
