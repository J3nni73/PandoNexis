using Litium.Accelerator.Routing;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using Litium.Web;
using Litium.Web.Administration.FieldFramework;
using Newtonsoft.Json;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using System.Globalization;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Processors
{
    [Service(ServiceType = typeof(PersonDataViewBase), Lifetime = DependencyLifetime.Transient, NamedService = true)]
    public abstract class PersonDataViewBase : IGenericDataViewProcessor
    {
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly GenericDataViewService _genericDataViewService;
        private readonly RequestModelAccessor _requestModelAccessor;

        protected PersonDataViewBase(FieldDefinitionService fieldDefinitionService,
                                        FieldTemplateService fieldTemplateService,
                                        GenericDataViewService genericDataViewService,
                                        RequestModelAccessor requestModelAccessor)
        {
            _fieldDefinitionService = fieldDefinitionService;
            _fieldTemplateService = fieldTemplateService;
            _genericDataViewService = genericDataViewService;
            _requestModelAccessor = requestModelAccessor;
        }

        public virtual Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDataForm(string data)
        {
            throw new NotImplementedException();
        }

        public abstract Task<GenericDataView> GetDataView(Guid pageSystemId, string data);


        public GenericDataViewSettings GetDataViewSettings(Guid pageSystemId)
        {
            return _genericDataViewService.GetDataViewSettings(pageSystemId);
        }
        public virtual GenericDataContainer GetFields(string templateId)
        {

            var container = new GenericDataContainer();
            var fieldTemplate = _fieldTemplateService.GetAll()?.FirstOrDefault(i => i.Id == templateId);
            if (fieldTemplate == null) { return container; }

            var availableFields = new List<string>();
            var editableFields = new List<string>();
            var requiredFields = new List<string>();
            if (fieldTemplate.GetType().Name == FieldTemplateHelperConstants.PersonFieldTemplate)
            {
                var personFieldTemplate = (PersonFieldTemplate)fieldTemplate;
                availableFields = personFieldTemplate?.FieldGroups?.FirstOrDefault(i => i.Id == ProcessorConstants.AvailableFields)?.Fields.ToList();
                if (availableFields == null) { return container; }
                editableFields = personFieldTemplate?.FieldGroups?.FirstOrDefault(i => i.Id == ProcessorConstants.EditableFields)?.Fields.ToList();
                requiredFields = personFieldTemplate?.FieldGroups?.FirstOrDefault(i => i.Id == ProcessorConstants.RequiredFields)?.Fields.ToList();
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
                
                dataField.Settings.Editable = editableFields?.Contains(field)??false;
                //dataField.Settings.IsRequired = requiredFields?.Contains(field)??false;
                dataField.Settings.ValidationRules = GetValidationRules(field, requiredFields);
                container.Fields.Add(dataField);
            }

            return container;
        }
        public virtual GenericDataContainer BuildContainer(GenericDataContainer templateContainer, Person person)
        {
            var result = JsonConvert.DeserializeObject<GenericDataContainer>(JsonConvert.SerializeObject(templateContainer));

            foreach (var field in result.Fields)
            {
                if (person.Fields.TryGetValue(field.FieldId, CultureInfo.CurrentCulture, out var value))
                {
                    field.FieldValue = value?.ToString() ?? string.Empty;
                }
                else if (person.Fields.TryGetValue(field.FieldId, out var value2))
                {
                    field.FieldValue = value2?.ToString() ?? string.Empty;
                }
                field.EntitySystemId = person.SystemId.ToString();
            }

            return result;

        }
        public List<ValidationRule> GetValidationRules(string fieldDefinitionId, List<string> requiredFields)
        {
            
            var result = new List<ValidationRule>();
            if (requiredFields?.Contains(fieldDefinitionId) ?? false)
            {
                result.Add(new ValidationRule() {
                        Rule = ValidationRuleConstants.IsRequired, 
                        ErrorMessage = "addons.genericdataview.validationrulemessage.isrequired".AsWebsiteText(_requestModelAccessor.RequestModel.WebsiteModel.Website) });
            }
            if (fieldDefinitionId==SystemFieldDefinitionConstants.Email)
            {
                result.Add(new ValidationRule()
                {
                    Rule = ValidationRuleConstants.Email,
                    ErrorMessage = "addons.genericdataview.validationrulemessage.email".AsWebsiteText(_requestModelAccessor.RequestModel.WebsiteModel.Website)
                });
            }

            return result;
        }
        public Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }

        public Task<object> HandleFormData(string data)
        {
            throw new NotImplementedException();
        }

        public abstract Task<GenericDataContainer> UpdateField(GenericDataField fieldData);
        
        
    }
}
