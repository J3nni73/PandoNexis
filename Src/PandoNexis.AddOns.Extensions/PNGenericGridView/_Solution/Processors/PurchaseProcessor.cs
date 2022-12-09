using PandoNexis.AddOns.Extensions.PNGenericGridView.Objects;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Services;
using Litium.Accelerator.Routing;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Constants;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Processors;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.Processors
{
    [Service(Name = "Purchase")]
    public class PurchaseProcessor : GridViewDataProcessorBase
    {
        private const string _processorName = ProductTemplateNameConstants.Purchase;
        private readonly GenericGridViewService _genericGridViewService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly ExcelExportService _excelExportService;
        private int? _pageSize = null;

        public int PageSize
        {
            get
            {
                if (!_pageSize.HasValue)
                {
                    if (_requestModelAccessor.RequestModel.CurrentPageModel.Page.Fields.TryGetValue("PageSize", out var pageSizeValue))
                    {
                        _pageSize = (int)pageSizeValue;
                    }
                    else
                    {
                        _pageSize = 50;
                    }
                }
                return (int)_pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        public PurchaseProcessor(GenericGridViewService genericGridViewService, ExcelExportService excelExportService, RequestModelAccessor requestModelAccessor)
        {
            _genericGridViewService = genericGridViewService;
            _excelExportService = excelExportService;
            _requestModelAccessor = requestModelAccessor;
        }

        public override async Task<object> HandleFormData(string data)
        {
            throw new NotImplementedException();
        }

        public override async Task<object> GetGridForm(string q)
        {
            var fields = _genericGridViewService.GetFieldsToForm(_processorName);
            return fields;
        }
        public override async Task<object> GetGridView(string q)
        {

            var gridView = new GenericGridView
            {
                DataRows = new List<GenericGridViewRow>(),
                Settings = new GenericGridViewSettings(50, 50)
            };

            var variantSystemIds = _genericGridViewService.BuildAsync(_processorName)?.Result;
            if (variantSystemIds == null || variantSystemIds.Count == 0) return gridView;
            var fields = _genericGridViewService.GetFields(_processorName);
            if (fields == null || fields.Count == 0) return gridView;
            gridView.DataRows.AddRange(_genericGridViewService.BuildProductRows(variantSystemIds, fields));

            //var test = JsonConvert.SerializeObject(gridView);
            //UpdateRow("{\"EntitySystemId\": \"2cd8f61d-f4f7-4211-25c5-08d9e2f7ff30\",\"UnitPriceUSD\": \"2.5\"}");
            return gridView;
        }

        public override async Task<object> UpdateRow(string data)
        {
            return _genericGridViewService.UpdateProduct(data, _processorName);
        }

        public override async Task<object> GetGridViewForExport(string data)
        {
            var oldPageSize = PageSize;
            PageSize = 100000;
            var gridView = await GetGridView(data);
            PageSize = oldPageSize;
            return _excelExportService.CreateExcelPackage(_genericGridViewService.BuildRowsForExport((GenericGridView)gridView));
        }
    }
}
