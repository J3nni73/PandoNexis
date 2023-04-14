﻿using Litium.FieldFramework;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Web.Administration.FieldFramework;
using Newtonsoft.Json;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;
using System.Globalization;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Processors
{
    [Service(ServiceType = typeof(ProductDataViewBase), Lifetime = DependencyLifetime.Transient, NamedService = true)]
    public abstract class ProductDataViewBase : IGenericDataViewProcessor
    {
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly FieldDefinitionService _fieldDefinitionService;

        protected ProductDataViewBase(FieldTemplateService fieldTemplateService, 
                                        FieldDefinitionService fieldDefinitionService)
        {
            _fieldTemplateService = fieldTemplateService;
            _fieldDefinitionService = fieldDefinitionService;
        }

        public abstract Task<object> GetDataForm(string data);


        public abstract Task<GenericDataView> GetDataView(Guid pageSystemId, string data);

        public abstract Task<object> GetGridViewForExport(string data);

        public abstract Task<object> HandleFormData(string data);

        public abstract Task<object> UpdateRow(string data);
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
                    FieldID = fieldDefinition.Id,
                    FieldName = fieldDefinition.GetEntityName(CultureInfo.CurrentCulture),
                    FieldType = fieldDefinition.FieldType,
                };
                dataField.Settings.Editable = editableFields?.Contains(field) != null ? true : false;


                container.Fields.Add(dataField);
            }

            return container;
        }
        
        public GenericDataContainer BuildContainer(GenericDataContainer templateContainer, Variant variant)
        {
            var test = JsonConvert.SerializeObject(templateContainer);
            var test2 = JsonConvert.DeserializeObject<GenericDataContainer>(test);
            var result = test2;

            foreach (var field in result.Fields)
            {
                if (variant.Fields.TryGetValue(field.FieldID, CultureInfo.CurrentCulture, out var value))
                    field.FieldValue = value?.ToString()??string.Empty;
                field.EntitySystemId = variant.SystemId.ToString();
            }

            return result;
        }

        public Task<GenericDataContainer> ButtonClick(GenericDataField fieldData)
        {
            throw new NotImplementedException();
        }

        public GenericDataViewSettings GetDataViewSettings(Guid pageSystemId)
        {
            throw new NotImplementedException();
        }
    }
}