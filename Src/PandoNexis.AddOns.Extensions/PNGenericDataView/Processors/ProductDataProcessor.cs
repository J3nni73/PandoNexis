using Litium.Accelerator.Routing;
using Litium.Accelerator.Search;
using Litium.Accelerator.ViewModels.Search;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNGenericDataView.Objects;

namespace PandoNexis.AddOns.Extensions.PNGenericDataView.Processors
{
    [Service(Name = "ProductData")]
    public class ProductDataProcessor : ProductDataViewBase
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly ProductSearchService _productSearchService;

        private const string AreaSource = "ProductData";
        public ProductDataProcessor(FieldTemplateService fieldTemplateService,
                                    RequestModelAccessor requestModelAccessor,
                                    ProductSearchService productSearchService,
                                    FieldDefinitionService fieldDefinitionService) : base(fieldTemplateService, fieldDefinitionService)
        {
            _requestModelAccessor = requestModelAccessor;
            _productSearchService = productSearchService;
        }

        public override async Task<object> GetDataForm(string data)
        {
            throw new NotImplementedException();
        }

        public override async Task<GenericDataView> GetDataView(Guid pageSystemId, string data)
        {
            var dataView = new GenericDataView();
            //Get fields to display
            var templateContainer = GetFields(AreaSource);

            //Get products to display
            var searchQuery = _requestModelAccessor.RequestModel.SearchQuery.Clone();
            searchQuery.PageSize = dataView.Settings.PageSize;

            var searchResults = await _productSearchService.SearchAsync(searchQuery, searchQuery.Tags, true, true, true);
            if (searchResults == null)
            {
                return dataView;
            }

            var dataContainers = new List<GenericDataContainer>();

            //Create DataView 
            var test = searchResults.Items.Value.Cast<ProductSearchResult>();
            foreach (var item in test)
            {
                if (item != null)
                {
                    var resOrigin = BuildContainer(templateContainer, item.Item.SelectedVariant);
                    var res = new GenericDataContainer
                    {
                        Fields = new List<GenericDataField>(resOrigin.Fields),
                        Messages = resOrigin.Messages
                    };

                    dataContainers.Add(res);
                }
            }


            dataView.DataContainers = dataContainers;

            return dataView;
        }

        public override async Task<object> GetGridViewForExport(string data)
        {
            throw new NotImplementedException();
        }

        public override async Task<object> HandleFormData(string data)
        {
            throw new NotImplementedException();
        }

        public override async Task<object> UpdateRow(string data)
        {
            throw new NotImplementedException();
        }
    }
}
