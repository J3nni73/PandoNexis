using Litium.Customers;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.PNPilot.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.PNPilot.Definitions
{
    internal class PilotCustomerFieldTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetOrganizationField(PilotFieldTemplateConstants.PilotCustomer, "General", SystemFieldDefinitionConstants.NameInvariantCulture ),
                GetOrganizationField(PilotFieldTemplateConstants.PilotCustomer, "General", SystemFieldDefinitionConstants.Description ),
                GetOrganizationField(PilotFieldTemplateConstants.PilotCustomer, "General", PilotFieldNameConstants.ErpId ),
                GetOrganizationField(PilotFieldTemplateConstants.PilotCustomer, "General", PilotFieldNameConstants.WorkItemPrefix),
                GetOrganizationField(PilotFieldTemplateConstants.PilotCustomer, "General", PilotFieldNameConstants.NextId),
                GetOrganizationField(PilotFieldTemplateConstants.PilotCustomer, "Logs", ContactLoggConstants.ContactLogg),
            };

            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new OrganizationFieldTemplate(PilotFieldTemplateConstants.PilotCustomer)
            {
                FieldGroups = new[]
                  {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,

                        }
                    }
            };
            return template;
        }
    }
}
