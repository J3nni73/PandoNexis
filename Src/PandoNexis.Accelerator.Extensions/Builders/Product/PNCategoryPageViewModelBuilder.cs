using Litium.Accelerator.Builders.LandingPage;
using Litium.Accelerator.Builders.Product;
using Litium.Accelerator.Caching;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Search;
using Litium.Accelerator.ViewModels;
using Litium.Accelerator.ViewModels.Product;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.Accelerator.Extensions.ViewModels.Product;

namespace PandoNexis.Accelerator.Extensions.Builders.Product
{
    public class PNCategoryPageViewModelBuilder : CategoryPageViewModelBuilder
    {
        private readonly CategoryService _categoryService;
        private readonly LandingPageViewModelBuilder _landingPageViewModelBuilder;
        private readonly ProductItemViewModelBuilder _productItemBuilder;
        private readonly ProductSearchService _productSearchService;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly PageByFieldTemplateCache<LandingPageByFieldTemplateCache> _landingPageByFieldTemplateCache;
        private readonly IEnumerable<PNCategoryPageModelService> _pnCategoryPageModelService;

        public PNCategoryPageViewModelBuilder(CategoryService categoryService,
            ProductItemViewModelBuilder productItemBuilder,
            LandingPageViewModelBuilder landingPageViewModelBuilder,
            ProductSearchService productSearchService,
            RequestModelAccessor requestModelAccessor,
            PageByFieldTemplateCache<LandingPageByFieldTemplateCache> landingPageByFieldTemplateCache,
            IEnumerable<PNCategoryPageModelService> pnCategoryPageModelService)
            : base(categoryService,
            productItemBuilder,
            landingPageViewModelBuilder,
            productSearchService,
            requestModelAccessor,
            landingPageByFieldTemplateCache)
        {
            _categoryService = categoryService;
            _productItemBuilder = productItemBuilder;
            _landingPageViewModelBuilder = landingPageViewModelBuilder;
            _productSearchService = productSearchService;
            _requestModelAccessor = requestModelAccessor;
            _landingPageByFieldTemplateCache = landingPageByFieldTemplateCache;
            _pnCategoryPageModelService = pnCategoryPageModelService;
        }

        public async override Task<CategoryPageViewModel> BuildAsync(Guid categorySystemId, DataFilterBase dataFilter = null)
        {
            var model = await base.BuildAsync(categorySystemId, dataFilter);
            var viewModel = model.MapTo<PNCategoryPageViewModel>();

            foreach (var item in _pnCategoryPageModelService)
            {
                item.BuildPartialModel(ref viewModel);
            }

            return viewModel;
        }
    }
}
