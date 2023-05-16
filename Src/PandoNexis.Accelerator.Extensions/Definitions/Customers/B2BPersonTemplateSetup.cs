using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Litium.Accelerator.Constants;
using Litium.AspNetCore.RequestTimeFeature;
using Litium.Customers;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Constants;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;

namespace PandoNexis.Accelerator.Extensions.Definitions.Customers
{
    internal class B2BPersonTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>()
            {
            GetPersonField(Litium.Accelerator.Constants.CustomerTemplateIdConstants.B2BPersonTemplate, FieldFrameworkConstants.GeneralEn, SystemFieldDefinitionConstants.FirstName),
            GetPersonField(Litium.Accelerator.Constants.CustomerTemplateIdConstants.B2BPersonTemplate, FieldFrameworkConstants.GeneralEn, SystemFieldDefinitionConstants.LastName),
            GetPersonField(Litium.Accelerator.Constants.CustomerTemplateIdConstants.B2BPersonTemplate, FieldFrameworkConstants.GeneralEn, SystemFieldDefinitionConstants.Email),
            GetPersonField(Litium.Accelerator.Constants.CustomerTemplateIdConstants.B2BPersonTemplate, FieldFrameworkConstants.GeneralEn, SystemFieldDefinitionConstants.Phone),
            };
            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new PersonFieldTemplate(Litium.Accelerator.Constants.CustomerTemplateIdConstants.B2BPersonTemplate)
            {
                FieldGroups = new[]
                 {
                        new FieldTemplateFieldGroup()
                        {
                            Id = FieldFrameworkConstants.GeneralEn,
                            Collapsed = false,

                        }
                    }
            };
            return template;
        }
    }
}
