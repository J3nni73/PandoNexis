using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Services;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Searchers;
using Litium.Customers;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Processors;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.Processors
{
    [Service(Name = "Quota")]
    public class QuotaProcessor : GridViewDataProcessorBase
    {
        private const string _processorName = ProductTemplateNameConstants.Quota;
        private readonly GenericGridViewService _genericGridViewService;
        private readonly SecurityContextService _securityContextService;

        private readonly IEnumerable<BaseSearcher> _searchers;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly CategoryService _categoryService;

        private readonly ChannelService _channelService;
        private readonly VariantService _variantService;
        private readonly BaseProductService _baseProductService;
        private readonly GroupService _groupService;

        public QuotaProcessor(GenericGridViewService genericGridViewService, IEnumerable<BaseSearcher> searchers,
            RequestModelAccessor requestModelAccessor, CategoryService categoryService,
             GroupService groupService, ChannelService channelService, VariantService variantService,
             SecurityContextService securityContextService, BaseProductService baseProductService)
        {
            _genericGridViewService = genericGridViewService;
            _searchers = searchers;
            _requestModelAccessor = requestModelAccessor;
            _categoryService = categoryService;

            _groupService = groupService;
            _channelService = channelService;
            _variantService = variantService;
            _securityContextService = securityContextService;
            _baseProductService = baseProductService;

        }

        public override async Task<object> HandleFormData(string data)
        {
            var items = JsonConvert.DeserializeObject<JObject>(data);

            var website = _requestModelAccessor.RequestModel.WebsiteModel;
            //var category = website.GetValue<PointerItem>(PageFieldNameConstants.QuotaCategory);
            var category = _categoryService.Get(_processorName);

            var variantSystemIds = new List<Guid>();
            if (category != null)
            {
                var categoryName = items.Properties().First().Value.ToString();
                var result = await _genericGridViewService.GetAllVariantsFromCatAsync(category.SystemId, categoryName);
                variantSystemIds.AddRange(result);
            }
            var gridView = new GenericGridView
            {
                DataRows = new List<GenericGridViewRow>(),
                Settings = new GenericGridViewSettings(50, 50)
            };


            if (variantSystemIds == null || variantSystemIds.Count == 0) return gridView;
            var fields = _genericGridViewService.GetFields(_processorName);
            if (fields == null || fields.Count == 0) return gridView;
            gridView.DataRows.AddRange(_genericGridViewService.BuildProductRows(variantSystemIds, fields));

            //var test = JsonConvert.SerializeObject(gridView);
            //foreach (var row in _genericGridViewService.GetTestRows())
            //{
            //    UpdateRow(row);
            //}
            return gridView;

        }

        public override async Task<object> GetGridForm(string q)
        {
            var fields = _genericGridViewService.GetFieldsToForm(_processorName);
            return fields;
        }
        public override async Task<object> GetGridView(string q)
        {
            var category = _categoryService.Get(_processorName);
            var gridView = new GenericGridView
            {
                DataRows = new List<GenericGridViewRow>(),
                Settings = new GenericGridViewSettings(50, 50)
            };
            if (category == null)
            {
                var variantSystemIds = _genericGridViewService.BuildAsync(_processorName)?.Result;
                if (variantSystemIds == null || variantSystemIds.Count == 0) return gridView;
            };

            var id = q.ToLower().Contains("id") ? q.Split('=')[1] : String.Empty;
            if (string.IsNullOrEmpty(id))
            {
                return gridView;
            }

            return HandleFormData("{\"QuotaFormField\":\"" + id + "\"}");
            //return genericGridView.DataRows;
            //var variantSystemIds = _genericGridViewService.BuildAsync("*")?.Result;
            //if (variantSystemIds == null || variantSystemIds.Count == 0) return gridView;
            //var fields = _genericGridViewService.GetFields(_processorName);
            //if (fields == null || fields.Count == 0) return gridView;
            //gridView.DataRows.AddRange(_genericGridViewService.BuildProductRows(variantSystemIds, fields));

            //var test = JsonConvert.SerializeObject(gridView);
            //foreach (var row in _genericGridViewService.GetTestRows())
            //{
            //    UpdateRow(row);
            //}
            // return gridView;
        }
        public override async Task<object> UpdateRow(string data)
        {
            return _genericGridViewService.UpdateProduct(data, _processorName);
        }

        public override Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }
    }
}
