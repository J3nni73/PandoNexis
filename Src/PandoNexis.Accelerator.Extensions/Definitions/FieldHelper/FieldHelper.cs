using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Websites;

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
                case "ProductArea":
                    return _fieldDefinitionService.Get<ProductArea>(fieldDefinitionId);
                case "WebsiteArea":
                    return _fieldDefinitionService.Get<WebsiteArea>(fieldDefinitionId);
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
    }
}
