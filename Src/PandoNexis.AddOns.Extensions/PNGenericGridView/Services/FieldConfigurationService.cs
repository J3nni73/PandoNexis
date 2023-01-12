using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Common;
using Litium.FieldFramework;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Web.Administration.FieldFramework;
using PandoNexis.AddOns.Extensions.PNGenericGridView.ViewModels;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Services
{
    [Service(ServiceType = typeof(FieldConfigurationService), Lifetime = DependencyLifetime.Singleton)]
    public class FieldConfigurationService
    {
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly PersonStorage _personStorage;
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly SettingService _settingService;
        private readonly RequestModelAccessor _requestModelAccessor;

        public FieldConfigurationService(FieldTemplateService fieldTemplateService, PersonStorage personStorage, FieldDefinitionService fieldDefinitionService,
            SettingService settingService, RequestModelAccessor requestModelAccessor)
        {
            _fieldTemplateService = fieldTemplateService;
            _personStorage = personStorage;
            _fieldDefinitionService = fieldDefinitionService;
            _settingService = settingService;
            _requestModelAccessor = requestModelAccessor;
        }
        public List<FieldConfigurationField> GetDefaultFields(List<FieldConfigurationField> fields, string type)
        {
            List<FieldConfigurationField> defaultFields = new List<FieldConfigurationField>();
            var fieldTemplate = _fieldTemplateService.Get<ProductFieldTemplate>(type);
            if (fieldTemplate != null && _personStorage.CurrentSelectedOrganization != null)
            {
                var defaultBaseProductFields = fieldTemplate.ProductFieldGroups.FirstOrDefault(i => i.Id == "Default");
                var defaultVariantFields = fieldTemplate.VariantFieldGroups.FirstOrDefault(i => i.Id == "Default");

                foreach (var field in fields)
                {
                    if (field.IsBaseProduct && defaultBaseProductFields?.Fields?.Any(i => i == field.Id) == true)
                    {
                        defaultFields.Add(field);
                    }
                    else if (!field.IsBaseProduct && defaultVariantFields?.Fields?.Any(i => i == field.Id) == true)
                    {
                        defaultFields.Add(field);
                    }
                }
            }
            return defaultFields;
        }

        public List<FieldConfigurationField> GetFields(string type)
        {
            List<FieldConfigurationField> fields = new List<FieldConfigurationField>();
            var template = _fieldTemplateService.Get<ProductFieldTemplate>(type);
            if (template != null)
            {
                AddBaseProductFields(template, fields);
                AddVariantFields(template, fields);
            }

            return fields;
        }

        private void AddBaseProductFields(ProductFieldTemplate template, List<FieldConfigurationField> fields)
        {
            List<string> fieldsToExport = new List<string>();
            List<string> editableFields = new List<string>();
            foreach (var group in template.ProductFieldGroups.Where(i => i.Id == "Fields"))
            {
                fieldsToExport.AddRange(@group.Fields);
            }

            var editableBaseProductFields = template.ProductFieldGroups.FirstOrDefault(i => i.Id == "Editable");

            foreach (var field in fieldsToExport)
            {
                // Get the fielddefinition
                var fieldDefinition = _fieldDefinitionService.Get<ProductArea>(field);

                if (fieldDefinition != null)
                {
                    if (fieldDefinition.FieldType == SystemFieldTypeConstants.Pointer ||
                        fieldDefinition.FieldType == SystemFieldTypeConstants.MultiField)
                        continue;

                    var exportField = new FieldConfigurationField
                    {
                        Id = field,
                        Name = fieldDefinition.GetEntityName(CultureInfo.CurrentCulture),
                        IsBaseProduct = true,
                        IsEditable = editableBaseProductFields?.Fields?.Any(i => i == field) ?? false
                    };
                    // Add the item ExportFieldsdata to the list
                    fields.Add(exportField);
                }
            }
        }

        private void AddVariantFields(ProductFieldTemplate template, List<FieldConfigurationField> fields)
        {
            List<string> fieldsToExport = new List<string>();
            foreach (var group in template.VariantFieldGroups.Where(i => i.Id == "Fields"))
            {
                fieldsToExport.AddRange(@group.Fields);
            }

            var editableVariantFields = template.VariantFieldGroups.FirstOrDefault(i => i.Id == "Editable");

            foreach (var field in fieldsToExport)
            {
                // Get the fielddefinition
                var fieldDefinition = _fieldDefinitionService.Get<ProductArea>(field);

                if (fieldDefinition != null)
                {
                    if (fieldDefinition.FieldType == SystemFieldTypeConstants.Pointer ||
                        fieldDefinition.FieldType == SystemFieldTypeConstants.MultiField)
                        continue;

                    var exportField = new FieldConfigurationField
                    {
                        Id = field,
                        Name = fieldDefinition.GetEntityName(CultureInfo.CurrentCulture),
                        IsBaseProduct = false,
                        IsEditable = editableVariantFields?.Fields?.Any(i => i == field) ?? false
                    };
                    // Add the item ExportFieldsdata to the list
                    fields.Add(exportField);
                }
            }
        }
    }
}
