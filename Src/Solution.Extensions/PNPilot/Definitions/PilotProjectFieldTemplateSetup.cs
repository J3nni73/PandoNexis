using Litium.Customers;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using Solution.Extensions.PNPilot.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Definitions
{
    internal class PilotProjectFieldTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
               GetOrganizationField(PilotFieldTemplateConstants.PilotProject, "General", SystemFieldDefinitionConstants.NameInvariantCulture ),
               GetOrganizationField(PilotFieldTemplateConstants.PilotProject, "General", SystemFieldDefinitionConstants.Description ),
               GetOrganizationField(PilotFieldTemplateConstants.PilotProject, "General", PilotFieldNameConstants.Customer ),
               GetOrganizationField(PilotFieldTemplateConstants.PilotProject, "General", PilotFieldNameConstants.ProjectType ),
               GetOrganizationField(PilotFieldTemplateConstants.PilotProject, "General", PilotFieldNameConstants.AddOns ),

            };

            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new OrganizationFieldTemplate(PilotFieldTemplateConstants.PilotProject)
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
