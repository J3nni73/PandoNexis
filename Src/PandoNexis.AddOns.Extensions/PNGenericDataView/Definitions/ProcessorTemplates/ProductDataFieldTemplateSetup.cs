using Litium.Accelerator.Constants;
using Litium.FieldFramework;
using Litium.Products;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Definitions.ProcessorTemplates
{
    internal class ProductDataFieldTemplateSetup : FieldTemplateHelper
    {
        
        public ProductDataFieldTemplateSetup()
        {
            
        }
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>()
            {
                GetProductField(ProcessorConstants.ProductData,FieldTemplateHelperConstants.VariantFieldGroups, ProcessorConstants.AvailableFields,SystemFieldDefinitionConstants.Name),
                GetProductField(ProcessorConstants.ProductData,FieldTemplateHelperConstants.VariantFieldGroups, ProcessorConstants.AvailableFields,SystemFieldDefinitionConstants.Description),
                GetProductField(ProcessorConstants.ProductData,FieldTemplateHelperConstants.VariantFieldGroups, ProcessorConstants.AvailableFields,ProductFieldNameConstants.Brand),
                GetProductField(ProcessorConstants.ProductData,FieldTemplateHelperConstants.VariantFieldGroups, ProcessorConstants.AvailableFields,ProductFieldNameConstants.Color),
                GetProductField(ProcessorConstants.ProductData,FieldTemplateHelperConstants.VariantFieldGroups, ProcessorConstants.AvailableFields,ProductFieldNameConstants.Size),

                GetProductField(ProcessorConstants.ProductData,FieldTemplateHelperConstants.VariantFieldGroups, ProcessorConstants.DefaultFields,SystemFieldDefinitionConstants.Name),
                GetProductField(ProcessorConstants.ProductData,FieldTemplateHelperConstants.VariantFieldGroups, ProcessorConstants.DefaultFields,SystemFieldDefinitionConstants.Description),
                GetProductField(ProcessorConstants.ProductData,FieldTemplateHelperConstants.VariantFieldGroups, ProcessorConstants.DefaultFields,ProductFieldNameConstants.Brand),
                GetProductField(ProcessorConstants.ProductData,FieldTemplateHelperConstants.VariantFieldGroups, ProcessorConstants.DefaultFields,ProductFieldNameConstants.Color),
                GetProductField(ProcessorConstants.ProductData,FieldTemplateHelperConstants.VariantFieldGroups, ProcessorConstants.DefaultFields,ProductFieldNameConstants.Size),

                GetProductField(ProcessorConstants.ProductData,FieldTemplateHelperConstants.VariantFieldGroups, ProcessorConstants.EditableFields,SystemFieldDefinitionConstants.Name),

            };


            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            

            var template = new ProductFieldTemplate(ProcessorConstants.ProductData)
            {
                VariantFieldGroups = new[]
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
