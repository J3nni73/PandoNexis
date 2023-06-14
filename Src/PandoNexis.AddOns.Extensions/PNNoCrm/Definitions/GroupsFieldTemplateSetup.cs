using Litium.Customers;
using Litium.FieldFramework;
using Litium.Products;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;
using PandoNexis.AddOns.Extensions.PNRegisterMe.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNNoCrm.Definitions
{
    internal class GroupsFieldTemplateSetup : FieldTemplateHelper
    {
        private readonly DisplayTemplateService _displayTemplateService;
        public GroupsFieldTemplateSetup(DisplayTemplateService displayTemplateService)
        {
            _displayTemplateService = displayTemplateService;
        }
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>()
            {
               GetGroupField(NoCrmProcessorConstants.Groups, ProcessorConstants.AvailableFields, SystemFieldDefinitionConstants.NameInvariantCulture),
               GetGroupField(NoCrmProcessorConstants.Groups, ProcessorConstants.AvailableFields, SystemFieldDefinitionConstants.Description),
            };


            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {

            var template = new GroupFieldTemplate(NoCrmProcessorConstants.Groups)
            {
                FieldGroups = new[]
                     {
                        new FieldTemplateFieldGroup
                        {
                            Id = ProcessorConstants.AvailableFields,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = ProcessorConstants.AvailableFields },
                                ["en-US"] = { Name = ProcessorConstants.AvailableFields }
                            },
                            UseInStorefront = true,
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = ProcessorConstants.DefaultFields,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = ProcessorConstants.DefaultFields },
                                ["en-US"] = { Name = ProcessorConstants.DefaultFields }
                            },

                            UseInStorefront = true,
                        },
                        new FieldTemplateFieldGroup
                        {
                            Id = ProcessorConstants.DefaultFields,
                            Collapsed = false,
                            Localizations =
                            {
                                ["sv-SE"] = { Name = ProcessorConstants.EditableFields },
                                ["en-US"] = { Name = ProcessorConstants.EditableFields }
                            },

                            UseInStorefront = true,
                        }
                },
            };
            return template;
        }
    }
}
