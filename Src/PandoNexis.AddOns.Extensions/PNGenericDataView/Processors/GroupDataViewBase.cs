using Litium.Accelerator.Routing;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using Litium.Web.Administration.FieldFramework;
using Newtonsoft.Json;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Processors
{
    [Service(ServiceType = typeof(GroupDataViewBase), Lifetime = DependencyLifetime.Transient, NamedService = true)]
    public abstract class GroupDataViewBase : IGenericDataViewProcessor
    {
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly GenericDataViewService _genericDataViewService;
        private readonly RequestModelAccessor _requestModelAccessor;
        public Guid _currentPageSystemId { get; set; }
        public GroupDataViewBase(FieldTemplateService fieldTemplateService,
            FieldDefinitionService fieldDefinitionService,
            GenericDataViewService genericDataViewService,
            RequestModelAccessor requestModelAccessor)
        {
            _fieldTemplateService = fieldTemplateService;
            _fieldDefinitionService = fieldDefinitionService;
            _genericDataViewService = genericDataViewService;
            _requestModelAccessor = requestModelAccessor;
        }

        public abstract Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data);
        public abstract Task<object> GetDataForm(string data);
        public abstract Task<GenericDataView> GetDataView(Guid pageSystemId, string data);
        public abstract GenericDataViewSettings GetDataViewSettings(Guid pageSystemId);
        public virtual GenericDataContainer GetFields(string templateId)
        {
            var container = new GenericDataContainer();
            var fieldTemplate = _fieldTemplateService.GetAll()?.FirstOrDefault(i => i.Id == templateId);
            if (fieldTemplate == null) { return container; }

            var availableFields = new List<string>();
            var editableFields = new List<string>();
            var requiredFields = new List<string>();
            if (fieldTemplate.GetType().Name == FieldTemplateHelperConstants.GroupFieldTemplate)
            {
                var groupFieldTemplate = (GroupFieldTemplate)fieldTemplate;
                availableFields = groupFieldTemplate?.FieldGroups?.FirstOrDefault(i => i.Id == ProcessorConstants.AvailableFields)?.Fields.ToList();
                if (availableFields == null) { return container; }
                editableFields = groupFieldTemplate?.FieldGroups?.FirstOrDefault(i => i.Id == ProcessorConstants.EditableFields)?.Fields.ToList();
                requiredFields = groupFieldTemplate?.FieldGroups?.FirstOrDefault(i => i.Id == ProcessorConstants.RequiredFields)?.Fields.ToList();
            }
          
            if (availableFields == null && availableFields.Any()) { return container; }


            foreach (var field in availableFields)
            {
                var fieldDefinition = _fieldDefinitionService.Get<CustomerArea>(field);
                var dataField = new GenericDataField
                {
                    FieldId = fieldDefinition.Id,
                    FieldName = fieldDefinition.GetEntityName(CultureInfo.CurrentCulture),
                    FieldType = _genericDataViewService.GetDataViewFieldType(fieldDefinition.FieldType),
                    EntitySystemId = Guid.Empty.ToString(),

                };

                dataField.Settings.Editable = editableFields?.Contains(field) ?? false;
                //dataField.Settings.IsRequired = requiredFields?.Contains(field)??false;
                dataField.Settings.ValidationRules = GetValidationRules(field, requiredFields);
                container.Fields.Add(dataField);
            }

            return container;
        }
        public abstract Task<object> GetGridViewForExport(string data);
        public abstract Task<object> HandleFormData(string data);
        public abstract Task<GenericDataContainer> UpdateField(GenericDataField fieldData);
        public List<ValidationRule> GetValidationRules(string fieldDefinitionId, List<string> requiredFields)
        {

            var result = _genericDataViewService.GetValidationRules(fieldDefinitionId, requiredFields);

            return result;
        }
        public virtual GenericDataContainer BuildContainer(GenericDataContainer templateContainer, Group group)
        {
            var result = JsonConvert.DeserializeObject<GenericDataContainer>(JsonConvert.SerializeObject(templateContainer));

            foreach (var field in result.Fields)
            {
                if (group.Fields.TryGetValue(field.FieldId, CultureInfo.CurrentCulture, out var value))
                { 
                    field.FieldValue = value?.ToString() ?? string.Empty; 
                }
                else if (group.Fields.TryGetValue(field.FieldId, out var value2))
                {
                    field.FieldValue = value2?.ToString() ?? string.Empty;
                }
                field.EntitySystemId = group.SystemId.ToString();
            }

            return result;

        }
    }
    
}
