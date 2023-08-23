using Litium.FieldFramework;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Web.Administration.FieldFramework;
using Newtonsoft.Json;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Services;
using System.Globalization;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Processors
{
    [Service(ServiceType = typeof(ProductDataViewBase), Lifetime = DependencyLifetime.Transient, NamedService = true)]
    public abstract class ProductDataViewBase : IGenericDataViewProcessor
    {
        private readonly GenericDataViewService _genericDataViewService;
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly FieldDefinitionService _fieldDefinitionService;
        public Guid _currentPageSystemId { get; set; }
        protected ProductDataViewBase(FieldTemplateService fieldTemplateService,
                                        FieldDefinitionService fieldDefinitionService,
                                        GenericDataViewService genericDataViewService)
        {
            _fieldTemplateService = fieldTemplateService;
            _fieldDefinitionService = fieldDefinitionService;
            _genericDataViewService = genericDataViewService;
        }

        public abstract Task<GenericDataView> GetDataView(Guid pageSystemId, string data);

        public abstract Task<object> GetGridViewForExport(string data);

        public abstract Task<GenericDataContainer> UpdateField(GenericDataField fieldData);
        public virtual GenericDataContainer GetFields(string templateId)
        {
            var container = new GenericDataContainer();
            var fieldTemplate = _fieldTemplateService.Get<ProductFieldTemplate>(templateId);
            if (fieldTemplate == null) { return container; }
            var availableFields = fieldTemplate?.VariantFieldGroups?.FirstOrDefault(i => i.Id == ProcessorConstants.AvailableFields)?.Fields;
            if (availableFields == null) { return container; }
            var editableFields = fieldTemplate?.VariantFieldGroups?.FirstOrDefault(i => i.Id == ProcessorConstants.EditableFields)?.Fields;

            foreach (var field in availableFields)
            {
                var fieldDefinition = _fieldDefinitionService.Get<ProductArea>(field);
                var dataField = new GenericDataField
                {
                    FieldId = fieldDefinition.Id,
                    FieldName = fieldDefinition.GetEntityName(CultureInfo.CurrentCulture),
                    FieldType = _genericDataViewService.GetDataViewFieldType( fieldDefinition.FieldType),
                };
                dataField.Settings.Editable = editableFields?.Contains(field) != null ? true : false;


                container.Fields.Add(dataField);
            }

            return container;
        }
        
        public GenericDataContainer BuildContainer(GenericDataContainer templateContainer, Variant variant)
        {
            var result = JsonConvert.DeserializeObject<GenericDataContainer>(JsonConvert.SerializeObject(templateContainer));

            foreach (var field in result.Fields)
            {
                if (variant.Fields.TryGetValue(field.FieldId, CultureInfo.CurrentCulture, out var value))
                    field.FieldValue = value?.ToString()??string.Empty;
                field.EntitySystemId = variant.SystemId.ToString();
            }

            return result;
        }

        public Task<object> ButtonClick(Guid pageSystemId, string buttonId, string data)
        {
            throw new NotImplementedException();
        }

        public GenericDataViewSettings GetDataViewSettings(Guid pageSystemId)
        {
            throw new NotImplementedException();
        }
    }
}
