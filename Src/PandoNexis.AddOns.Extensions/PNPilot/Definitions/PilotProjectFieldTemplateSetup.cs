using Litium.Customers;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using PandoNexis.AddOns.Extensions.PNPilot.Constants;
using PandoNexis.AddOns.PNPilot.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.PNPilot.Definitions
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
               
               GetWebsiteField("AcceleratorWebsite", PilotProcessorConstants.Pilot, PilotFieldNameConstants.PilotAdminGroup), 
               GetWebsiteField("AcceleratorWebsite", PilotProcessorConstants.Pilot, PilotProcessorConstants.PilotButtonLinks),
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
