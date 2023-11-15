using Litium.Accelerator.Builders.Framework;
using Litium.Accelerator.Builders.Product;
using Litium.Accelerator.Builders.Search;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Search;
using Litium.Accelerator.ViewModels.Framework;
using Litium.Accelerator.ViewModels.Search;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.Accelerator.Extensions.ViewModels.Search;

namespace PandoNexis.Accelerator.Extensions.Builders.Search
{
    [Service(ServiceType = typeof(SearchResultViewModelBuilder), Lifetime = DependencyLifetime.Scoped)]
    public class PNSearchResultViewModelBuilder : SearchResultViewModelBuilder
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly ProductSearchService _productSearchService;
        private readonly ProductItemViewModelBuilder _productItemBuilder;
        private readonly PageSearchService _pageSearchService;
        private readonly CategorySearchService _categorySearchService;
        private readonly IEnumerable<PNSearchResultModelService> _pnSearchResultModelService;

        public PNSearchResultViewModelBuilder(RequestModelAccessor requestModelAccessor, ProductSearchService productSearchService, ProductItemViewModelBuilder productItemBuilder,
           PageSearchService pageSearchService, CategorySearchService categorySearchService, IEnumerable<PNSearchResultModelService> pnSearchResultModelService) : base(requestModelAccessor, productSearchService, productItemBuilder,
           pageSearchService, categorySearchService)
        {
            _requestModelAccessor = requestModelAccessor;
            _productSearchService = productSearchService;
            _productItemBuilder = productItemBuilder;
            _pageSearchService = pageSearchService;
            _categorySearchService = categorySearchService;
            _pnSearchResultModelService = pnSearchResultModelService;
        }

        public override async Task<SearchResultViewModel> BuildAsync()
        {
            var model = await base.BuildAsync();
            var viewModel = model.MapTo<PNSearchResultViewModel>();

            foreach (var item in _pnSearchResultModelService)
            {
                 item.BuildPartialModelAsync(ref viewModel);
            }

            return viewModel;
        }

    }
}
