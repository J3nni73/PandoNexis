using System.Globalization;
using JetBrains.Annotations;
using Litium.Accelerator.Builders.Product;
using Litium.Accelerator.Fields;
using Litium.Accelerator.ViewModels;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Media;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Web.Models;
using Litium.Web.Models.Products;
using Litium.Web.Routing;
using PandoNexis.Accelerator.Extensions.Services;

namespace PandoNexis.Accelerator.Extensions.Builders.Product
{
    public class PNProductFieldViewModelBuilder : ProductFieldViewModelBuilder
    {
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly NamedServiceFactory<FieldFormatter> _fieldFormatterServiceFactory;
        private readonly RouteRequestLookupInfoAccessor _routeRequestLookupInfoAccessor;
        private readonly SecurityContextService _securityContextService;
        private readonly PersonService _personService;
        private readonly FileService _fileService;
        private readonly PNFieldService _PNFieldService;
        private readonly PNSecurityService _PNSecurityService;

        public PNProductFieldViewModelBuilder(FieldDefinitionService fieldDefinitionService, NamedServiceFactory<FieldFormatter> fieldFormatterServiceFactory, PersonService personService, SecurityContextService securityContextService, FileService fileService, PNFieldService pNFieldService, PNSecurityService pNSecurityService, RouteRequestLookupInfoAccessor routeRequestLookupInfoAccessor)
             : base(fieldDefinitionService, fieldFormatterServiceFactory, routeRequestLookupInfoAccessor)
        {
            _fieldDefinitionService = fieldDefinitionService;
            _fieldFormatterServiceFactory = fieldFormatterServiceFactory;
            _personService = personService;
            _securityContextService = securityContextService;
            _fileService = fileService;
            _PNFieldService = pNFieldService;
            _PNSecurityService = pNSecurityService;
            _routeRequestLookupInfoAccessor = routeRequestLookupInfoAccessor;
        }

        public IEnumerable<ProductFieldViewModel> Build([NotNull] ProductModel productModel, [NotNull] string fieldGroup, bool includeBaseProductFields = true, bool includeVariantFields = true, bool includeHiddenFields = false, bool includeEmptyFields = false)
            => Build(productModel, fieldGroup, CultureInfo.CurrentUICulture, includeBaseProductFields, includeVariantFields, includeHiddenFields, includeEmptyFields);

        public IEnumerable<ProductFieldViewModel> Build([NotNull] ProductModel productModel, [NotNull] string fieldGroup, [NotNull] CultureInfo cultureInfo, bool includeBaseProductFields = true, bool includeVariantFields = true, bool includeHiddenFields = false, bool includeEmptyFields = false)
        {
            var result = new List<ProductFieldViewModel>();

            var personSystemId = _securityContextService.GetIdentityUserSystemId();
            var person = personSystemId == null ? _personService.Get(SecurityContextService.Everyone.Id) : _personService.Get(personSystemId.Value);
            //var identity = _securityContextService.CreateClaimsIdentity(person.LoginCredential.Username, person.SystemId);

            if (includeBaseProductFields)
            {
                var baseProductFields = productModel.FieldTemplate.ProductFieldGroups?.FirstOrDefault(x => fieldGroup.Equals(x.Id, StringComparison.OrdinalIgnoreCase))?.Fields;
                if (baseProductFields != null)
                {
                    foreach (var field in baseProductFields)
                    {
                        var fieldDefinition = _fieldDefinitionService.Get<ProductArea>(field);
                        if (fieldDefinition == null || fieldDefinition.Hidden && !includeHiddenFields)
                        {
                            continue;
                        }

                        var culture = fieldDefinition.MultiCulture ? cultureInfo.Name : "*";
                        if (productModel.BaseProduct.Fields.TryGetValue(field, culture, out var value))
                        {
                            result.Add(CreateModel(fieldDefinition, cultureInfo, value, person));
                        }
                        else if (includeEmptyFields)
                        {
                            result.Add(CreateModel(fieldDefinition, cultureInfo, culture, person));
                        }
                    }
                }
            }

            if (includeVariantFields)
            {
                var variantFields = productModel.FieldTemplate.VariantFieldGroups?.FirstOrDefault(x => fieldGroup.Equals(x.Id, StringComparison.OrdinalIgnoreCase))?.Fields;
                if (variantFields != null)
                {
                    foreach (var field in variantFields)
                    {
                        var fieldDefinition = _fieldDefinitionService.Get<ProductArea>(field);
                        if (fieldDefinition == null || fieldDefinition.Hidden && !includeHiddenFields)
                        {
                            continue;
                        }

                        var culture = fieldDefinition.MultiCulture ? cultureInfo.Name : "*";
                        if (productModel.SelectedVariant.Fields.TryGetValue(field, culture, out var value))
                        {
                            if (fieldDefinition.FieldType == SystemFieldTypeConstants.MultiField)
                            {
                                result.AddRange(CreateMultiFieldModel(fieldDefinition, cultureInfo, value, person));
                            }
                            else
                            {
                                result.Add(CreateModel(fieldDefinition, cultureInfo, value, person));
                            }

                        }
                        else if (includeEmptyFields)
                        {
                            result.Add(CreateModel(fieldDefinition, cultureInfo, culture, person));
                        }
                    }
                }
            }

            return result.Where(x => x != null);
        }

        private List<ProductFieldViewModel> CreateMultiFieldModel(FieldDefinition fieldDefinition, CultureInfo cultureInfo, object value, Person person)
        {
            var productFieldViewModelList = new List<ProductFieldViewModel>();
            var multiFieldList = new List<MultiFieldItem>();
            var fielddefinitions = _PNFieldService.GetMultiFieldDefinitionList(fieldDefinition);
            if (value is IEnumerable<MultiFieldItem>)
            {
                multiFieldList = (value as IEnumerable<MultiFieldItem>).ToList();
            }
            else
            {
                multiFieldList.Add((MultiFieldItem)value);
            }

            foreach (var item in multiFieldList)
            {
                foreach (var fieldDefinitionItem in fielddefinitions)
                {
                    var valueItem = item.Fields.GetValue<Object>(fieldDefinitionItem.Id) ?? item.Fields.GetValue<Object>(fieldDefinitionItem.Id, cultureInfo.Name); 
                    var model = CreateModel(fieldDefinitionItem, cultureInfo, valueItem, person);
                    productFieldViewModelList.Add(model);
                }
            }

            return productFieldViewModelList;
        }

        private ProductFieldViewModel CreateModel([NotNull] FieldDefinition fieldDefinition, CultureInfo cultureInfo, object value = null, Person person = null)
        {
            var fieldFormatter = _fieldFormatterServiceFactory.GetService(fieldDefinition.FieldType);

            var fileType = fieldDefinition.FieldType;
            if (fieldFormatter == null)
            {
                return null;
            }
            if (fileType == SystemFieldTypeConstants.MediaPointerFile || fileType == SystemFieldTypeConstants.MediaPointerImage)
            {
                if (!_PNSecurityService.HasFileAccess(person, value))
                {
                    return null;
                }
            }
            if (fileType == SystemFieldTypeConstants.MediaPointerFile)
            {
                return CreateModel("FileField", fieldDefinition, cultureInfo, new MediaResourceFieldFormatArgs { Culture = cultureInfo }, fieldFormatter, value, true);
            }

            if (fileType == SystemFieldTypeConstants.MediaPointerImage)
            {
                return CreateModel("ImageField", fieldDefinition, cultureInfo, new MediaResourceFieldFormatArgs { Culture = cultureInfo }, fieldFormatter, value, true);
            }

            if (fileType == SystemFieldTypeConstants.Editor)
            {
                return CreateModel("Field", fieldDefinition, cultureInfo, new FieldFormatArgs { Culture = cultureInfo }, fieldFormatter, value.MapTo<EditorString>().Value);
            }

            return CreateModel("Field", fieldDefinition, cultureInfo, new FieldFormatArgs { Culture = cultureInfo }, fieldFormatter, value);

        }
        private ProductFieldViewModel CreateModel(string viewName, FieldDefinition fieldDefinition, CultureInfo cultureInfo, FieldFormatArgs fieldFormatArgs, FieldFormatter fieldFormatter, object value = null, bool checkAccess = false)
        {
            var productFieldViewModel = new ProductFieldViewModel
            {
                ViewName = viewName,
                Name = fieldDefinition.Localizations[cultureInfo].Name,
                Value = fieldFormatter.Format(fieldDefinition, value, fieldFormatArgs),
                Args = fieldFormatArgs
            };

            return productFieldViewModel;
        }
    }

}
