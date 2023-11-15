using Litium.Accelerator.Builders.Block;
using Litium.Accelerator.Builders.Product;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Search;
using Litium.Accelerator.ViewModels.Block;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Web.Models.Blocks;
using Litium.Web.Models.Products;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.Accelerator.Extensions.ViewModels.Block;

namespace PandoNexis.Accelerator.Extensions.Builders.Block
{
    [Service(ServiceType = typeof(ProductBlockViewModelBuilder), Lifetime = DependencyLifetime.Scoped)]
    public class PNProductBlockViewModelBuilder : ProductBlockViewModelBuilder
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly ProductModelBuilder _productModelBuilder;
        private readonly VariantService _variantService;
        private readonly BaseProductService _baseProductService;
        private readonly ProductSearchService _productSearchService;
        private readonly ProductItemViewModelBuilder _productItemViewModelBuilder;
        private readonly IEnumerable<PNProductBlockModelService> _pnProductBlockModelService;

        public PNProductBlockViewModelBuilder(ProductModelBuilder productModelBuilder,
            ProductItemViewModelBuilder productItemViewModelBuilder,
            RequestModelAccessor requestModelAccessor,
            ProductSearchService productSearchService,
            BaseProductService baseProductService,
            VariantService variantService,
            IEnumerable<PNProductBlockModelService> pnProductBlockModelService) : base(productModelBuilder,
            productItemViewModelBuilder,
            requestModelAccessor,
            productSearchService,
            baseProductService,
            variantService)
        {
            _productModelBuilder = productModelBuilder;
            _productItemViewModelBuilder = productItemViewModelBuilder;
            _requestModelAccessor = requestModelAccessor;
            _productSearchService = productSearchService;
            _baseProductService = baseProductService;
            _variantService = variantService;
            _pnProductBlockModelService = pnProductBlockModelService;
        }

        /// <summary>
        /// Build the product block view model
        /// </summary>
        /// <param name="blockModel">The current product block</param>
        /// <returns>Return the product block view model</returns>
        public override async Task<ProductBlockViewModel> BuildAsync(BlockModel blockModel)
        {
            var data = blockModel.MapTo<ProductBlockViewModelData>();
            if (string.IsNullOrEmpty(data.Title))
            {
                data.Title = string.Empty;
            }

            var model = await base.BuildAsync(blockModel);
            var viewModel = model.MapTo<PNProductBlockViewModel>();

            foreach (var item in _pnProductBlockModelService)
            {
                item.BuildPartialModel(ref viewModel, data);
            }
            return viewModel;
        }
    }
}
