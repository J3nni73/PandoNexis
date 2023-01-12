using DocumentFormat.OpenXml;
using Litium;
using Litium.Accelerator;
using Litium.Accelerator.Routing;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Web.Models;
using Litium.Web.Models.Products;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Globalization;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericGridView.FieldTypes;
using PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Processors;
using Litium.Accelerator.Definitions;
using Microsoft.Extensions.Logging;
using Litium.Accelerator.Utilities;
using System.IO;
using Litium.Accelerator.Search;
using Litium.Accelerator.ViewModels.Search;
using Litium.Web;
using Litium.Application.Tagging.Data;
using Litium.Web.Administration.EditorHandlers;
using Litium.Products.Queryable;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Services
{
    [Service(ServiceType = typeof(GenericGridViewService), Lifetime = DependencyLifetime.Transient)]
    public class GenericGridViewService
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly VariantService _variantService;
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly IServiceProvider _iServiceProvider;
        private readonly SecurityContextService _securityContextService;
        private readonly LanguageService _languageService;
        private readonly CategoryService _categoryService;
        private readonly ProductModelBuilder _productModelBuilder;
        private readonly BaseProductService _baseProductService;
        private readonly AssortmentService _assortmentService;
        private readonly ChannelService _channelService;
        private readonly FilterAggregator _filterAggregator = IoC.Resolve<FilterAggregator>();
        private readonly ILogger<AcceleratorDefaultPermissionSetup> _logger;

        public GenericGridViewService(RequestModelAccessor requestModelAccessor,
                                        FieldTemplateService fieldTemplateService,
                                        VariantService variantService,
                                        FieldDefinitionService fieldDefinitionService,
                                        IServiceProvider iServiceProvider,
                                        SecurityContextService securityContextService,
                                        LanguageService languageService, CategoryService categoryService, ProductModelBuilder productModelBuilder,
                                        BaseProductService baseProductService, AssortmentService assortmentService, ChannelService channelService, ILogger<AcceleratorDefaultPermissionSetup> logger)
        {
            _requestModelAccessor = requestModelAccessor;
            _fieldTemplateService = fieldTemplateService;
            _variantService = variantService;
            _fieldDefinitionService = fieldDefinitionService;
            _iServiceProvider = iServiceProvider;
            _securityContextService = securityContextService;
            _languageService = languageService;
            _categoryService = categoryService;
            _productModelBuilder = productModelBuilder;
            _baseProductService = baseProductService;
            _assortmentService = assortmentService;
            _channelService = channelService;
            _logger = logger;
        }

        public string GetTagName(string type)
        {
            //if (type == "ArticleNumber")
            //    return TagNames.VariantArticleNumbers;
            return null;
        }

        public GenericGridView BuildErrorRow()
        {
            var rows = new List<GenericGridViewRow>();
            var row = new GenericGridViewRow { Fields = new List<GenericGridViewField>() };
            GenericGridViewFieldSettings settings = new GenericGridViewFieldSettings { ReadOnly = true };
            var newField = new GenericGridViewField
            {
                EntitySystemId = Guid.Empty.ToString(),
                FieldID = Guid.Empty.ToString(),
                FieldName = "Du måste välja lastningsdag.",
                FieldType = "string",
                FieldValue = "Välj lastningsdag uppe till höger.",
                FieldSuffix = "",
                Settings = settings
            };
            row.Fields.Add(newField);
            rows.Add(row);
            return new GenericGridView
            {
                DataRows = rows,
                Settings = new GenericGridViewSettings(50, 50)
                {
                    Filter = BuildProductFilter(null, new List<string>() { "" }, false)
                }
            };
        }

        public List<GenericGridViewRow> BuildProductRows(List<Guid> variantSystemIds, List<GenericGridViewField> fields)
        {
            var rows = new List<GenericGridViewRow>();

            foreach (var variantSystemId in variantSystemIds)
            {

                rows.Add(BuildProductRow(variantSystemId, fields));
            }
            return rows;
        }

        public GenericGridViewRow BuildProductRow(Guid variantSystemId, List<GenericGridViewField> fields)
        {
            var row = new GenericGridViewRow()
            {
                Fields = new List<GenericGridViewField>()
            };
            var variant = _variantService.Get(variantSystemId);

            //var image = (variant?.Fields.GetValue<IList<Guid>>(SystemFieldDefinitionConstants.Images)?.FirstOrDefault()
            //   ?? baseProduct?.Fields.GetValue<IList<Guid>>(SystemFieldDefinitionConstants.Images)?.FirstOrDefault()).MapTo<ImageModel>();

            //var iimage = image?.GetUrlToImage(Size.Empty, new Size(200, 120)).Url;

            foreach (var field in fields)
            {
                GenericGridViewFieldSettings settings = new GenericGridViewFieldSettings { ReadOnly = !field.Settings.Editable }; // Change IsEditable to Setting.Editable Todo check if work...
                string fieldType = "string";
                string value = string.Empty;
                var fieldDefinition = _fieldDefinitionService.Get<ProductArea>(field.FieldID);
                var specialFieldData = IoC.ResolvePlugin<IGridSpecialFieldData>(field.FieldID, false);

                if (specialFieldData != null)
                {
                    //settings = specialFieldData.GetSettings(field);
                    settings = field.Settings;
                    fieldType = specialFieldData.GetGridViewFieldType();
                    //value = variant.GetValue(field.FieldID, false, fieldDefinition);
                    value = specialFieldData.GetValue(field, variant);
                }
                else
                {
                    if (fieldDefinition != null)
                    {
                        fieldType = fieldDefinition.FieldType.GetGridViewFieldType();
                        value = variant.GetValue(field.FieldID, false, fieldDefinition);
                        //value = variant.GetValue(field.FieldID, fieldDefinition.MultiCulture, customerData, fieldDefinition);

                    }
                }

                var newFields = new GenericGridViewField();
                newFields.EntitySystemId = variant.SystemId.ToString();
                newFields.FieldID = field.FieldID;
                newFields.FieldName = field.FieldName;
                newFields.FieldValue = value;
                newFields.FieldType = fieldType;
                newFields.Settings = settings;
                if (fieldType == "dropdown" || fieldType == "checkbox")
                {
                    newFields.DropDownOptions = GetDropDownOptions(field.FieldID);

                }
                if (fieldType == "productimageupload")
                {
                    newFields.DropDownOptions = GetArticleImages(variant);//image?.GetUrlToImage(Size.Empty, new Size(200, 120)).Url;
                    //newFields.FieldValue = image.Alt;
                }

                row.Fields.Add(newFields);
            }
            return row;
        }

        private List<GenericGridViewFieldSimpleList> GetArticleImages(Variant variant)
        {
            // Start Get Images
            var productModel = _productModelBuilder.BuildFromVariant(variant);
            var baseProduct = _baseProductService.Get(variant?.BaseProductSystemId ?? Guid.Empty);

            var image = (variant?.Fields.GetValue<IList<Guid>>(SystemFieldDefinitionConstants.Images)
               ?? baseProduct?.Fields.GetValue<IList<Guid>>(SystemFieldDefinitionConstants.Images));
            var newList = new List<GenericGridViewFieldSimpleList>();
            if (image != null)
            {
                foreach (var field in image)
                {
                    var result = field.MapTo<ImageModel>();
                    newList.Add(new GenericGridViewFieldSimpleList { Text = $"{result.Title}-id-{field}", Value = result.GetUrlToImage(Size.Empty, new Size(200, 120)).Url });
                }
            }
            return newList;
        }

        public List<GenericGridViewField> GetFields(string fieldTemplateId)
        {
            var result = new List<GenericGridViewField>();
            var fieldTemplate = _fieldTemplateService.Get<ProductFieldTemplate>(fieldTemplateId);
            if (fieldTemplate == null) return result;
            var fieldVariantFields = fieldTemplate.VariantFieldGroups.FirstOrDefault(i => i.Id == GenericGridView_ProductTemplateNameConstants.Fields).Fields;
            var editableVariantFields = fieldTemplate.VariantFieldGroups.FirstOrDefault(i => i.Id == GenericGridView_ProductTemplateNameConstants.Editable).Fields; ;
            foreach (var fieldVariantField in fieldVariantFields)
            {
                var fieldDefinition = _fieldDefinitionService.Get<ProductArea>(fieldVariantField);
                var specialFieldData = IoC.ResolvePlugin<IGridSpecialFieldData>(fieldVariantField, false);

                if (fieldDefinition == null) continue;
                if (fieldDefinition.FieldType == SystemFieldTypeConstants.Pointer || fieldDefinition.FieldType == SystemFieldTypeConstants.MultiField) continue;

                var field = new GenericGridViewField();
                field.FieldID = fieldVariantField;
                field.FieldName = fieldVariantField;//fieldDefinition.GetEntityName(CultureInfo.CurrentCulture);
                //field.FieldType = fieldDefinition.FieldType.GetGridViewFieldType();
                if (specialFieldData != null && fieldDefinition.FieldType == "SpecialField")
                {
                    field.FieldType = specialFieldData.GetGridViewFieldType() ?? "string";
                }
                else
                {
                    field.FieldType = fieldDefinition.FieldType.GetGridViewFieldType();
                }
                var editable = editableVariantFields.Contains(field.FieldID);
                field.Settings = new GenericGridViewFieldSettings()
                {
                    Editable = editable,
                    ReadOnly = editable == false,
                };
                if (fieldVariantField == GenericGridView_ProductFieldNameConstants.Category)
                {
                    field.DropDownOptions = GetDropDownOptions(fieldVariantField);
                    //field.FieldType = "dropdown";
                }

                if (field.FieldType == "checkbox")
                {
                    field.DropDownOptions = GetDropDownOptions(fieldVariantField);
                }
                result.Add(field);
            }
            return result;
        }

        public List<GenericGridViewField> GetFieldsToForm(string fieldTemplateId)
        {
            var result = new List<GenericGridViewField>();
            var fieldTemplate = _fieldTemplateService.Get<ProductFieldTemplate>(fieldTemplateId);
            if (fieldTemplate == null) return result;
            //var fieldVariantFields = fieldTemplate.VariantFieldGroups.FirstOrDefault(i => i.Id == ProductTemplateNameConstants.Fields).Fields;
            var editableVariantFields = fieldTemplate.VariantFieldGroups.FirstOrDefault(i => i.Id == GenericGridView_ProductTemplateNameConstants.Editable)?.Fields;
            var formGroupVariantFields = fieldTemplate.VariantFieldGroups.FirstOrDefault(i => i.Id == GenericGridView_ProductTemplateNameConstants.FormGroup)?.Fields;

            if (formGroupVariantFields == null)
            {
                return result;
            }
            foreach (var fieldVariantField in formGroupVariantFields)
            {
                var fieldDefinition = _fieldDefinitionService.Get<ProductArea>(fieldVariantField);
                var specialFieldData = IoC.ResolvePlugin<IGridSpecialFieldData>(fieldVariantField, false);
                if (fieldDefinition == null) continue;
                if (fieldDefinition.FieldType == SystemFieldTypeConstants.Pointer || fieldDefinition.FieldType == SystemFieldTypeConstants.MultiField) continue;


                var field = new GenericGridViewField();
                field.FieldID = fieldVariantField;
                field.FieldName = fieldVariantField;//fieldDefinition.GetEntityName(CultureInfo.CurrentCulture);
                //field.FieldType = fieldDefinition.FieldType.GetGridViewFieldType();
                if (fieldDefinition.FieldType == "SpecialField")
                {
                    field.FieldType = specialFieldData.GetGridViewFieldType();
                }
                else
                {
                    field.FieldType = fieldDefinition.FieldType.GetGridViewFieldType();
                }
                var editable = editableVariantFields.Contains(field.FieldID);
                field.Settings = new GenericGridViewFieldSettings()
                {
                    Editable = editable,
                    ReadOnly = editable == false,
                };
                if (fieldVariantField == GenericGridView_ProductFieldNameConstants.Category)
                {
                    field.DropDownOptions = GetDropDownOptions(fieldVariantField);
                    //field.FieldType = "dropdown";
                }

                if (field.FieldType == "checkbox")
                {
                    field.DropDownOptions = GetDropDownOptions(fieldVariantField);
                }
                result.Add(field);
            }
            return result;
        }

        public List<GenericGridViewFieldSimpleList> GetDropDownOptions(string fieldId)
        {
            List<GenericGridViewFieldSimpleList> dropDownOptions = new List<GenericGridViewFieldSimpleList>();
            if (fieldId == GenericGridView_ProductFieldNameConstants.Category)
            {
                //Get value from categories
                var categories = _categoryService.GetChildCategories(Guid.Parse("88820f7f-96c9-45fc-be1f-93048ffc814e"));
                foreach (var category in categories)
                {
                    var newDropDownItem = new GenericGridViewFieldSimpleList
                    {
                        Value = category.SystemId.ToString(),
                        Text = category.Fields.GetValue<string>(SystemFieldDefinitionConstants.Name, CultureInfo.CurrentCulture)
                    };
                    dropDownOptions.Add(newDropDownItem);
                }
            }
            else
            {
                //Get value from textoption field
                var fieldDefinition = _fieldDefinitionService.Get<ProductArea>(fieldId);
                var options = fieldDefinition?.Option as TextOption;
                if (options?.Items != null)
                {
                    foreach (var item in options.Items)
                    {
                        var newDropDownItem = new GenericGridViewFieldSimpleList
                        {
                            Value = item.Value,
                            Text = item.Name.Values.FirstOrDefault(x => x != string.Empty)
                        };
                        dropDownOptions.Add(newDropDownItem);
                    }

                }
            }
            return dropDownOptions;
        }

        public GenericGridViewRow UpdateProduct(string data, string type)
        {
            var item = JsonConvert.DeserializeObject<JObject>(data);

            var variantId = new Guid(item.GetValue("EntitySystemId").ToString());

            var variant = _variantService.Get(variantId)?.MakeWritableClone();
            if (variant == null)
                return null;

            foreach (var property in item.Properties().Where(x => x.Name != "EntitySystemId"))
            {
                var fieldDefinition = _fieldDefinitionService.Get<ProductArea>(property.Name);
                if (fieldDefinition == null)
                {
                    var splittedName = property.Name.Split('_')?[0];
                    fieldDefinition = _fieldDefinitionService.Get<ProductArea>(splittedName);
                    if (fieldDefinition == null)
                        return null;
                }

                var converter = GetStringFieldTypeConverter(fieldDefinition);
                var value = converter.ConvertFromString(fieldDefinition.Id, property.Value.ToString(), fieldDefinition);

                var specialFieldData = IoC.ResolvePlugin<IGridSpecialFieldData>(fieldDefinition.Id, false);

                if (fieldDefinition.FieldType == "SpecialField")
                {
                    var quantity = (decimal)property.Value;
                    if (fieldDefinition.Id == "Quantity" && quantity > 0)
                    {
                        var qtyCtn = variant.Fields.GetValue<decimal>(GenericGridView_ProductFieldNameConstants.QtyCtn);
                        if ((quantity % qtyCtn) != 0)
                        {
                            quantity = (((quantity - (quantity % qtyCtn)) / qtyCtn) + 1) * qtyCtn;
                        }
                    }
                    specialFieldData.AddSpecialFieldToCartAsync(variant.Id.ToString(), quantity);
                }
                else if (fieldDefinition.MultiCulture)
                {
                    var languages = _languageService.GetAll();
                    foreach (var lang in languages)
                    {
                        variant.Fields.AddOrUpdateValue(fieldDefinition.Id, lang.CultureInfo.Name, value);
                    }
                }
                else
                {
                    variant.Fields.AddOrUpdateValue(fieldDefinition.Id, value);
                }
            }

            // Skip groups / group permissions when we update
            using (_securityContextService.ActAsSystem("My custom integration task"))
            {
                variant.Fields.AddOrUpdateValue(GenericGridView_ProductFieldNameConstants.UpdatedDate, DateTime.UtcNow);
                _variantService.Update(variant);
            }

            return BuildProductRow(variant.SystemId, GetFields(type));
        }
        private StringFieldTypeConverter GetStringFieldTypeConverter(FieldDefinition fieldDefinition)
        {
            return _iServiceProvider.GetRequiredNamedService<StringFieldTypeConverter>(fieldDefinition.FieldType);
        }

        public virtual async Task<List<Guid>> BuildAsync(string query)
        {
            var category = _categoryService.Get(query);
            var assortment = _assortmentService.GetAll().FirstOrDefault();
            if (category == null)
            {
                category = await CreateCategory(query, query, assortment, null);
            }

            var result = new List<Guid>();
            // Aspen - test code to get test data.
            //category = _categoryService.Get(Guid.Parse("44f69e40-f4f2-4c28-bfae-ab67537f0013"));
            // Aspen - END test code to get test data.


            foreach (var id in category.ProductLinks)
            {
                result.AddRange(id.ActiveVariantSystemIds);
            }
            return result;
        }

        public virtual async Task<List<Guid>> GetAllVariantsFromCatAsync(Guid parentCategoryEntitySystemId, string childCategoryName)
        {
            var result = new List<Guid>();
            var category = _categoryService.GetChildCategories(parentCategoryEntitySystemId).Where(x => x.Fields.GetValue<string>(GenericGridView_ProductFieldNameConstants.QuotaId) == childCategoryName).FirstOrDefault();

            if (category == null) return result;

            foreach (var id in category.ProductLinks)
            {
                result.AddRange(id.ActiveVariantSystemIds);
            }
            return result;
        }

        public List<ExcelExportService.ExportRow> BuildRowsForExport(Objects.GenericGridView gridView)
        {
            var exceptionFields = _fieldTemplateService.Get<ProductFieldTemplate>("ExcelExceptions")?.VariantFieldGroups.SelectMany(i => i.Fields).ToList() ?? new List<string>();

            List<ExcelExportService.ExportRow> rows = new List<ExcelExportService.ExportRow>();
            //hdear
            var headerRow = new ExcelExportService.ExportRow
            {
                ExportPropertiesList = new List<ExcelExportService.ExportColumn>()
            };
            var firstRow = gridView.DataRows.FirstOrDefault();
            if (firstRow != null)
            {
                foreach (var header in firstRow.Fields.Where(i => !exceptionFields.Contains(i.FieldID)))
                {
                    headerRow.ExportPropertiesList.Add(new ExcelExportService.ExportColumn
                    {
                        ExportValue = header.FieldName,
                        StyleIndex = (UInt32Value)1U
                    });
                }
                rows.Add(headerRow);
            }
            foreach (var row in gridView.DataRows)
            {
                var excelRow = new ExcelExportService.ExportRow
                {
                    ExportPropertiesList = new List<ExcelExportService.ExportColumn>()
                };
                foreach (var value in row.Fields.Where(i => !exceptionFields.Contains(i.FieldID)))
                {
                    excelRow.ExportPropertiesList.Add(new ExcelExportService.ExportColumn
                    {
                        ExportValue = value.FieldValue
                    });
                }
                rows.Add(excelRow);
            }
            return rows;
        }

        private async Task<Category> CreateCategory(string categoryName, string categoryId, Assortment assortment, Category parent)
        {
            try
            {
                var categoryFieldTemplate = _fieldTemplateService.GetAll().OfType<CategoryFieldTemplate>().FirstOrDefault();
                var category = new Category(categoryFieldTemplate.SystemId, assortment.SystemId)
                {
                    AssortmentSystemId = assortment.SystemId,
                    Id = categoryId,
                    ParentCategorySystemId = parent?.SystemId ?? Guid.Empty
                };
                category = category.MakeWritableClone();

                foreach (var assortmentLocalization in assortment.Localizations)
                {
                    var culture = CultureInfo.GetCultureInfo(assortmentLocalization.Key);
                    category.Fields.AddOrUpdateValue("_name", culture, categoryName);
                    //category.Fields.AddOrUpdateValue("_url", culture, _suggestionService.Suggest(culture, categoryName));
                }

                using (_securityContextService.ActAsSystem("My custom integration task"))
                {
                    _categoryService.Create(category);
                    PublishRecursive(category);
                }

                return category;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Cannot create category '{categoryName}' ('{categoryId}') in parent category '{parent?.SystemId.ToString() ?? "NULL"}' - {exception}");
                throw;
            }
        }

        public void PublishRecursive(Category category)
        {
            foreach (var channel in _channelService.GetAll())
            {
                var categoryConnectionExists = category.ChannelLinks.Any(link => link.ChannelSystemId.Equals(channel.SystemId));
                if (!categoryConnectionExists)
                {
                    var writeCategory = category.MakeWritableClone();
                    writeCategory.ChannelLinks.Add(new CategoryToChannelLink(channel.SystemId));
                    _categoryService.Update(writeCategory);
                }

                foreach (var productLink in category.ProductLinks)
                {
                    foreach (var variant in _variantService.GetByBaseProduct(productLink.BaseProductSystemId))
                    {
                        var variantConnectionExists = variant.ChannelLinks.Any(link => link.ChannelSystemId.Equals(channel.SystemId));
                        if (!variantConnectionExists)
                        {
                            var writeVariant = variant.MakeWritableClone();
                            writeVariant.ChannelLinks.Add(new VariantToChannelLink(channel.SystemId));
                            _variantService.Update(writeVariant);
                        }
                    }
                }

                foreach (var childCategory in category.GetChildren())
                    PublishRecursive(childCategory);
            }
        }

        public List<GenericFilters> BuildProductFilter(SearchQuery searchQuery, IEnumerable<string> propertyNames, bool enableFreeTextSearch = false)
        {
            var genericFilters = new List<GenericFilters>();
            if (enableFreeTextSearch)
            {
                genericFilters.Add(new GenericFilters
                {
                    FieldID = "q",
                    FieldName = "FreeTextSearch".AsWebsiteText(),
                    FilterType = "string"
                });
            }
            //TODO: What to do here when we come from the article relation
            if (string.IsNullOrEmpty(searchQuery?.Text))
            {
                return genericFilters;
            }
            var filters = new FilterResult
            {
                Items = _filterAggregator.GetFilterAsync(searchQuery, propertyNames).Result.ToList()
            };
            foreach (var filter in filters.Items)
                genericFilters.Add(new GenericFilters
                {
                    FieldID = filter.Attributes["value"],
                    FieldName = filter.Name,
                    FilterType = "dropDown",
                    Values = filter.Links.OrderBy(z => z.Name).Select(i => i.Name).ToList()
                });
            return genericFilters;
        }
    }
}
