using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using Litium;
using Litium.Accelerator;
using Litium.Accelerator.Routing;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Web.Models.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Litium.Accelerator.Definitions;
using Microsoft.Extensions.Logging;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Services;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.Services
{
    [Service(ServiceType = typeof(QuotaService), Lifetime = DependencyLifetime.Singleton)]
    public class QuotaService
    {
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly GroupService _groupService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly CategoryService _categoryService;
        private readonly VariantService _variantService;
        private readonly SecurityContextService _securityContextService;
        private readonly AssortmentService _assortmentService;
        private readonly FieldTemplateService _fieldTemplateService;
        private readonly ChannelService _channelService;
        private readonly BaseProductService _baseProductService;
        private readonly GenericGridViewService _genericGridViewService;

        private readonly ILogger<AcceleratorDefaultPermissionSetup> _logger;
        private Guid _visitorGroupSystemId;


        public QuotaService(FieldDefinitionService fieldDefinitionService, GroupService groupService, RequestModelAccessor requestModelAccessor,
            CategoryService categoryService, VariantService variantService, SecurityContextService securityContextService, AssortmentService assortmentService,
            FieldTemplateService fieldTemplateService, ChannelService channelService, BaseProductService baseProductService, GenericGridViewService genericGridViewService, ILogger<AcceleratorDefaultPermissionSetup> logger)
        {
            //_visitorGroupSystemId = (_groupService.Get<Group>("Visitors") ?? _groupService.Get<Group>("Besökare"))?.SystemId ?? Guid.Empty;
            _fieldDefinitionService = fieldDefinitionService;
            _groupService = groupService;
            _requestModelAccessor = requestModelAccessor;
            _categoryService = categoryService;
            _variantService = variantService;
            _securityContextService = securityContextService;
            _assortmentService = assortmentService;
            _fieldTemplateService = fieldTemplateService;
            _channelService = channelService;
            _baseProductService = baseProductService;
            _genericGridViewService = genericGridViewService;
            _logger = logger;
        }


        public async Task<object> GetAddedFieldsToGridView(string type, string data)
        {
            var items = JsonConvert.DeserializeObject<List<VariantSystemIdes>>(data);
            var ids = items.Select(x => x.SystemIds).ToList();
            //var variantId = new Guid(items.GetValue("systemIds").ToString());
            var gridView = new GenericGridView
            {
                DataRows = new List<GenericGridViewRow>(),
                Settings = new GenericGridViewSettings(50, 50)
            };


            //if (variantSystemIds == null || variantSystemIds.Count == 0) return gridView;
            var fields = _genericGridViewService.GetFields(type);
            if (fields == null || fields.Count == 0) return gridView;
            gridView.DataRows.AddRange(_genericGridViewService.BuildProductRows(ids, fields));

            return gridView;
        }

        public async Task<object> CreateNewQuota(string type, string data)
        {
            var items = JsonConvert.DeserializeObject<JObject>(data);

            var parentCategory = _categoryService.Get(type);

            var website = _requestModelAccessor.RequestModel.WebsiteModel;
            var category = website.GetValue<PointerItem>(Constants.PageFieldNameConstants.QuotaCategory);

            if (category == null)
            {
                var assortmentSystemId = _requestModelAccessor.RequestModel.ChannelModel?.Channel?.MarketSystemId?.MapTo<MarketModel>()?.Market?.AssortmentSystemId;
                var assortment = _assortmentService.GetAll().First();
                if (!assortmentSystemId.HasValue) return null;
                //var parentCategory = _categoryService.Get(category.EntitySystemId);
                var categoryName = items.Properties().First().Value.ToString();

                var result = CreateCategory(categoryName, categoryName, assortment, parentCategory);

            }
            //var title = items.Properties().FirstOrDefault(x => x.Name == "Title").Value.ToString();

            //_categoryService.Create(category);
            return new object();
        }

        public async Task<object> RemoveArticleFromOuotaCategory(string type, string data)
        {
            var itemsListObj = JsonConvert.DeserializeObject<List<VariantsData>>(data);

            var parentCategory = _categoryService.Get(type);

            var website = _requestModelAccessor.RequestModel.WebsiteModel;
            var categorWeb = website.GetValue<PointerItem>(Constants.PageFieldNameConstants.QuotaCategory);
            var category = _categoryService.GetChildCategories(parentCategory.SystemId).Where(x => x.Fields.GetValue<string>(Constants.ProductFieldNameConstants.QuotaId).ToLower().Contains(itemsListObj[0].QuotaId.ToLower())).FirstOrDefault().MakeWritableClone();

            if (category != null)
            {
                try
                {
                    var removedProducts = new List<Variant>();
                    foreach (var item in itemsListObj)
                    {
                        var variant = _variantService.Get(item.ArticleNumber)?.MakeWritableClone();

                        if (variant != null)
                        {
                            var existingCategoryLink = _categoryService.GetByBaseProduct(variant.BaseProductSystemId).FirstOrDefault(i => i.SystemId == category.SystemId)?.MakeWritableClone();
                            var activeVariantInCategory = existingCategoryLink?.ProductLinks.FirstOrDefault(x => x.ActiveVariantSystemIds.Contains(variant.SystemId));

                            // Remove variant link from category if variant exist in category
                            if (activeVariantInCategory != null)
                            {
                                if (category.ProductLinks.Any(x => x.ActiveVariantSystemIds.Contains(variant.SystemId)))
                                {
                                    category.ProductLinks.FirstOrDefault(x => x.BaseProductSystemId == variant.BaseProductSystemId)?.ActiveVariantSystemIds.Remove(variant.SystemId);
                                }
                                using (_securityContextService.ActAsSystem("My custom integration task"))
                                {
                                    _categoryService.Update(category);
                                }
                                removedProducts.Add(variant);
                            }
                        }
                    }
                    return removedProducts;
                }
                catch (Exception exception)
                {
                    _logger.LogError($"Cannot create category '{category.SystemId}'  in parent category ' - {exception.Message}");
                    throw;
                }

            }
            return new object();
        }
        public async Task<object> AddArticleToQuotaCategory(string type, string data)
        {
            var itemsListObj = JsonConvert.DeserializeObject<List<VariantsData>>(data);

            var parentCategory = _categoryService.Get(type);
            var category = _categoryService.GetChildCategories(parentCategory.SystemId).Where(x => x.Fields.GetValue<string>(Constants.ProductFieldNameConstants.QuotaId).ToLower().Contains(itemsListObj[0].QuotaId.ToLower())).FirstOrDefault().MakeWritableClone();

            if (category != null)
            {
                try
                {
                    var addedProducts = new List<Variant>();
                    foreach (var item in itemsListObj)
                    {
                        var variant = _variantService.Get(item.ArticleNumber)?.MakeWritableClone();

                        if (variant == null)
                        {
                            var fieldTemplate = _fieldTemplateService.Get<ProductFieldTemplate>("ProductWithVariants");
                            var baseProduct = new BaseProduct("BPT_" + item.ArticleNumber, fieldTemplate.SystemId);
                            using (_securityContextService.ActAsSystem("My custom integration task"))
                            {
                                _baseProductService.Create(baseProduct);
                            }

                            var visitorGroupSystemId = (_groupService.Get<Group>("Visitors") ?? _groupService.Get<Group>("Besökare"))?.SystemId ?? Guid.Empty;
                            if (visitorGroupSystemId != Guid.Empty && !baseProduct.AccessControlList.Any(i => i.GroupSystemId == visitorGroupSystemId))
                            {
                                baseProduct.AccessControlList.Add(new AccessControlEntry(Operations.Entity.Read, visitorGroupSystemId));
                            }


                            variant = new Variant(item.ArticleNumber, baseProduct.SystemId) { SystemId = Guid.NewGuid() };
                            using (_securityContextService.ActAsSystem("My custom integration task"))
                            {
                                _variantService.Create(variant);
                            }
                            variant = variant.MakeWritableClone();
                            variant.Fields.AddOrUpdateValue(Constants.ProductFieldNameConstants.SupplierItemNumber, item.ArticleNumber);
                            variant.Fields.AddOrUpdateValue(Constants.ProductFieldNameConstants.CreatedDate, DateTime.UtcNow);
                            using (_securityContextService.ActAsSystem("My custom integration task"))
                            {
                                _variantService.Update(variant);
                            }
                        }

                        var existingCategoryLink = _categoryService.GetByBaseProduct(variant.BaseProductSystemId).FirstOrDefault(i => i.SystemId == category.SystemId)?.MakeWritableClone();
                        var activeVariantInCategory = existingCategoryLink?.ProductLinks.FirstOrDefault(x => x.ActiveVariantSystemIds.Contains(variant.SystemId));
                        //Place base product in the category if not exist
                        if (existingCategoryLink == null)
                        {
                            //var findBaseProdFromVariant = category.ProductLinks.All(x => x.ActiveVariantSystemIds.Contains(variant.BaseProductSystemId));

                            // Add baseproduct link and variant link to category if they not exist
                            if (category.ProductLinks.All(x => x.BaseProductSystemId != variant.BaseProductSystemId))
                            {
                                var link = new CategoryToProductLink(variant.BaseProductSystemId) { MainCategory = true };
                                link.ActiveVariantSystemIds.Add(variant.SystemId);

                                category.ProductLinks.Add(link);
                                using (_securityContextService.ActAsSystem("My custom integration task"))
                                {
                                    _categoryService.Update(category);
                                }
                                addedProducts.Add(variant);
                                //return variant;
                            }
                        }
                        // Add variant link to category if not variant exist in category
                        else if (activeVariantInCategory == null)
                        {
                            if (!category.ProductLinks.Any(x => x.ActiveVariantSystemIds.Contains(variant.SystemId)))
                            {
                                category.ProductLinks.FirstOrDefault(x => x.BaseProductSystemId == variant.BaseProductSystemId)?.ActiveVariantSystemIds.Add(variant.SystemId);
                            }
                            using (_securityContextService.ActAsSystem("My custom integration task"))
                            {
                                _categoryService.Update(category);
                            }
                            addedProducts.Add(variant);
                            //return variant;
                        }


                    }

                    return addedProducts;
                }
                catch (Exception exception)
                {
                    _logger.LogError($"Cannot create category '{category.SystemId}'  in parent category ' - {exception.Message}");
                    throw;
                }

            }
            return new object();
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
                    category.Fields.AddOrUpdateValue(Constants.ProductFieldNameConstants.QuotaId, categoryName);
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



        public class VariantsData
        {
            [JsonProperty("articleNumber")]
            public string ArticleNumber { get; set; }
            [JsonProperty("baseProductSystemId")]
            public string BaseProductSystemId { get; set; }
            [JsonProperty("quotaId")]
            public string QuotaId { get; set; }

        }

        public class VariantsDataJson
        {
            [JsonProperty("variantsData")]
            public VariantsData variantsData { get; set; }
        }
        public class VariantSystemIdes
        {
            [JsonProperty("systemIds")]
            public Guid SystemIds { get; set; }
        }
    }
}
