using Litium.Accelerator.ViewModels.MyPages;
using Litium.Blocks;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Websites;
using Microsoft.AspNetCore.Mvc.Rendering;
using PandoNexis.Accelerator.Extensions.Constants;
using System.ComponentModel.DataAnnotations;

namespace PandoNexis.Accelerator.Extensions.Definitions.FieldHelper
{
    [Service(ServiceType = typeof(FieldHelper))]
    public abstract class FieldHelper
    {
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly SecurityContextService _securityContextService;
        private readonly LanguageService _languageService;

        protected FieldHelper(FieldDefinitionService fieldDefinitionService,
            SecurityContextService securityContextService,
            LanguageService languageService)
        {
            _fieldDefinitionService = fieldDefinitionService;
            _securityContextService = securityContextService;
            _languageService = languageService;
        }



        public abstract void HandleFieldOptions();
        public abstract void HandleMultiFieldFields();


        public void UpdateMultiFieldField(List<FieldMultiFieldChanges> changes)
        {
            foreach (var change in changes)
            {
                var multiField = GetField(change.Area, change.FieldDefinitionId);
                if (multiField == null) continue;

                var fieldToAdd = GetField(change.Area, change.Field);
                if (fieldToAdd == null) continue;
                multiField = multiField.MakeWritableClone();
                var edited = false;
                if (!((MultiFieldOption)multiField.Option).Fields.Contains(fieldToAdd.Id))
                {
                    ((MultiFieldOption)multiField.Option).Fields.Add(fieldToAdd.Id);
                    edited = true;
                }
                if (edited)
                {
                    if (edited)
                    {
                        using (_securityContextService.ActAsSystem("Multifield task"))
                        {
                            _fieldDefinitionService.Update(multiField);
                        }
                    }
                }
            }
        }
        public void UpdateFieldOptions(List<FieldOptionChanges> changes)
        {
            foreach (var change in changes)
            {
                var field = GetField(change.Area, change.FieldDefinitionId);
                if (field == null) continue;
                if (field.FieldType != SystemFieldTypeConstants.TextOption) continue;

                field = field.MakeWritableClone();
                if (field.Option == null) field.Option = new TextOption();
                var edited = false;
                foreach (var option in change.Options)
                {
                    if (((TextOption)field.Option).Items.FirstOrDefault(i => i.Value == option.Value) == null)
                    {
                        ((TextOption)field.Option).Items.Add(option);
                        edited = true;
                    }
                }
                if (edited)
                {
                    using (_securityContextService.ActAsSystem("Textoption task"))
                    {
                        _fieldDefinitionService.Update(field);
                    }
                }


            }
        }
        public FieldDefinition GetField(string area, string fieldDefinitionId)
        {
            switch (area)
            {
                case nameof(ProductArea):
                    return _fieldDefinitionService.Get<ProductArea>(fieldDefinitionId);
                case nameof(WebsiteArea):
                    return _fieldDefinitionService.Get<WebsiteArea>(fieldDefinitionId);
                case nameof(BlockArea):
                    return _fieldDefinitionService.Get<BlockArea>(fieldDefinitionId);
                case nameof(CustomerArea):
                    return _fieldDefinitionService.Get<CustomerArea>(fieldDefinitionId);
                case nameof(GlobalizationArea):
                    return _fieldDefinitionService.Get<GlobalizationArea>(fieldDefinitionId);
            }
            return null;

        }

        public FieldOptionChanges GetFieldOption(string area, string fieldDefinitionId, string optionValue)
        {
            var result = new FieldOptionChanges();

            var names = new Dictionary<string, string>();
            foreach (var language in _languageService.GetAll())
            {
                names.Add(language.Id, optionValue);
            }
            result = new FieldOptionChanges
            {
                FieldDefinitionId = fieldDefinitionId,
                Area = area,
                Options = new List<TextOption.Item>
                        {
                            new TextOption.Item
                            {
                                Value = optionValue,
                                Name = names,
                            },

                        }
            };

            return result;

        }

        public FieldMultiFieldChanges GetMultiFieldChange(string area, string fieldDefinitionId, string addedfieldDefinitionId)
        {
            var result = new FieldMultiFieldChanges()
            {
                FieldDefinitionId = fieldDefinitionId,
                Area = area,
                Field = addedfieldDefinitionId
            };

            return result;
        }

        #region Product
        public FieldDefinition GetProductFieldDefinition(string fieldDefinitionId, string fieldType, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var fieldDefinition = new FieldDefinition<ProductArea>(fieldDefinitionId, fieldType)
            {
                CanBeGridColumn = gridColumn,
                CanBeGridFilter = gridFilter,
                MultiCulture = multiCulture,
                UseInStorefront = useInStorefront,
                Editable = editable,
            };
            return fieldDefinition;
        }
        public FieldDefinition GetProductTextOptionField(string fieldDefinitionId, string fieldType, bool manualSort = false, bool multiSelect = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var optionField = GetProductFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            optionField.Option = new TextOption
            {
                MultiSelect = multiSelect,
                ManualSort = manualSort,
                Items = new List<TextOption.Item>()
            };
            return optionField;
        }
        public FieldDefinition GetProductPointerField(string fieldDefinitionId, string fieldType, string pointerEntity, bool multiSelect = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var pointerField = GetProductFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            pointerField.Option = new PointerOption
            {
                EntityType = pointerEntity,
                MultiSelect = multiSelect
            };
            return pointerField;

        }

        public FieldDefinition GetProductMultiField(string fieldDefinitionId, string fieldType, bool isArray = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var multiField = GetProductFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            multiField.Option = new MultiFieldOption
            {
                IsArray = isArray,
                Fields = new List<string>()
            };
            return multiField;

        }
        #endregion

        #region Customer
        public FieldDefinition GetCustomerFieldDefinition(string fieldDefinitionId, string fieldType, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var fieldDefinition = new FieldDefinition<CustomerArea>(fieldDefinitionId, fieldType)
            {
                CanBeGridColumn = gridColumn,
                CanBeGridFilter = gridFilter,
                MultiCulture = multiCulture,
                UseInStorefront = useInStorefront,
                Editable = editable,
            };
            return fieldDefinition;
        }

        public FieldDefinition GetCustomerTextOptionField(string fieldDefinitionId, string fieldType, bool manualSort = false, bool multiSelect = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var optionField = GetCustomerFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            optionField.Option = new TextOption
            {
                MultiSelect = multiSelect,
                ManualSort = manualSort,
                Items = new List<TextOption.Item>()
            };
            return optionField;
        }
        public FieldDefinition GetCustomerPointerField(string fieldDefinitionId, string fieldType, string pointerEntity, bool multiSelect = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var pointerField = GetCustomerFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            pointerField.Option = new PointerOption
            {
                EntityType = pointerEntity,
                MultiSelect = multiSelect
            };
            return pointerField;

        }

        public FieldDefinition GetCustomerMultiField(string fieldDefinitionId, string fieldType, bool isArray = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var multiField = GetCustomerFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            multiField.Option = new MultiFieldOption
            {
                IsArray = isArray,
                Fields = new List<string>()
            };
            return multiField;

        }
        #endregion

        #region Website
        public FieldDefinition GetWebsiteFieldDefinition(string fieldDefinitionId, string fieldType, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var fieldDefinition = new FieldDefinition<WebsiteArea>(fieldDefinitionId, fieldType)
            {
                CanBeGridColumn = gridColumn,
                CanBeGridFilter = gridFilter,
                MultiCulture = multiCulture,
                UseInStorefront = useInStorefront,
                Editable = editable,
            };
            return fieldDefinition;
        }

        public FieldDefinition GetWebsiteTextOptionField(string fieldDefinitionId, string fieldType, bool manualSort = false, bool multiSelect = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var optionField = GetWebsiteFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            optionField.Option = new TextOption
            {
                MultiSelect = multiSelect,
                ManualSort = manualSort,
                Items = new List<TextOption.Item>()
            };
            return optionField;
        }
        public FieldDefinition GetWebsitePointerField(string fieldDefinitionId, string fieldType, string pointerEntity, bool multiSelect = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var pointerField = GetWebsiteFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            pointerField.Option = new PointerOption
            {
                EntityType = pointerEntity,
                MultiSelect = multiSelect
            };
            return pointerField;

        }

        public FieldDefinition GetWebsiteMultiField(string fieldDefinitionId, string fieldType, bool isArray = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var multiField = GetWebsiteFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            multiField.Option = new MultiFieldOption
            {
                IsArray = isArray,
                Fields = new List<string>()
            };
            return multiField;

        }
        #endregion

        #region Block
        public FieldDefinition GetBlockFieldDefinition(string fieldDefinitionId, string fieldType, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var fieldDefinition = new FieldDefinition<BlockArea>(fieldDefinitionId, fieldType)
            {
                CanBeGridColumn = gridColumn,
                CanBeGridFilter = gridFilter,
                MultiCulture = multiCulture,
                UseInStorefront = useInStorefront,
                Editable = editable,
            };
            return fieldDefinition;
        }
        public FieldDefinition GetBlockTextOptionField(string fieldDefinitionId, string fieldType, bool manualSort = false, bool multiSelect = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var optionField = GetBlockFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            optionField.Option = new TextOption 
            { 
                MultiSelect = multiSelect, 
                ManualSort  = manualSort, 
                Items = new List<TextOption.Item>()
            };
            return optionField;
        }
        public FieldDefinition GetBlockPointerField(string fieldDefinitionId, string fieldType, string pointerEntity, bool multiSelect = false,  bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var pointerField = GetBlockFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            pointerField.Option = new PointerOption
            {
                EntityType = pointerEntity, 
                MultiSelect = multiSelect
            };
            return pointerField;

        }

        public FieldDefinition GetBlockMultiField(string fieldDefinitionId, string fieldType, bool isArray = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var multiField = GetBlockFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            multiField.Option = new MultiFieldOption
            {
                IsArray = isArray,
                Fields = new List<string>()
            };
            return multiField;

        }
        #endregion


        #region Globalization
        public FieldDefinition GetGlobalizationFieldDefinition(string fieldDefinitionId, string fieldType, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var fieldDefinition = new FieldDefinition<GlobalizationArea>(fieldDefinitionId, fieldType)
            {
                CanBeGridColumn = gridColumn,
                CanBeGridFilter = gridFilter,
                MultiCulture = multiCulture,
                UseInStorefront = useInStorefront,
                Editable = editable,
            };
            return fieldDefinition;
        }
        public FieldDefinition GetGlobalizationTextOptionField(string fieldDefinitionId, string fieldType, bool manualSort = false, bool multiSelect = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var optionField = GetGlobalizationFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            optionField.Option = new TextOption
            {
                MultiSelect = multiSelect,
                ManualSort = manualSort,
                Items = new List<TextOption.Item>()
            };
            return optionField;
        }
        public FieldDefinition GetGlobalizationPointerField(string fieldDefinitionId, string fieldType, string pointerEntity, bool multiSelect = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var pointerField = GetGlobalizationFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            pointerField.Option = new PointerOption
            {
                EntityType = pointerEntity,
                MultiSelect = multiSelect
            };
            return pointerField;

        }

        public FieldDefinition GetGlobalizationMultiField(string fieldDefinitionId, string fieldType, bool isArray = false, bool multiCulture = false, bool gridColumn = false, bool gridFilter = false, bool editable = true, bool useInStorefront = true)
        {
            var multiField = GetGlobalizationFieldDefinition(fieldDefinitionId, fieldType, multiCulture, gridColumn, gridFilter, editable, useInStorefront);
            multiField.Option = new MultiFieldOption
            {
                IsArray = isArray,
                Fields = new List<string>()
            };
            return multiField;

        }
        #endregion

    }
}
