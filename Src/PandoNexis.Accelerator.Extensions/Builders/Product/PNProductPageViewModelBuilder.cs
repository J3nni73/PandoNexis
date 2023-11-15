using Litium.Accelerator.Caching;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Services;
using Litium.Accelerator.ViewModels.Product;
using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Sales;
using Litium.Web.Models.Products;
using Litium.Accelerator.Builders.Product;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.Accelerator.Extensions.ViewModels.Product;
using PandoNexis.Accelerator.Extensions.Services.Product;

namespace PandoNexis.Accelerator.Extensions.Builders.Product
{
    [Service(ServiceType = typeof(ProductPageViewModelBuilder), Lifetime = DependencyLifetime.Transient)]
    public class PNProductPageViewModelBuilder : ProductPageViewModelBuilder
    {
        private readonly ProductItemViewModelBuilder _itemViewModelBuilder;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly Litium.Accelerator.Services.ProductService _productService;
        private readonly PageByFieldTemplateCache<BrandPageFieldTemplateCache> _brandPageByFieldTypeCache;
        private readonly ProductFieldViewModelBuilder _productFieldViewModelBuilder;
        private readonly ProductModelBuilder _productModelBuilder;
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly ProductFieldGroupViewModelBuilder _productFieldGroupViewModelBuilder;
        private readonly IEnumerable<PNProductPageModelService> _pnProductPageModelService;
        private readonly PNProductPriceService _pnProductPriceService;

        public PNProductPageViewModelBuilder(
            ProductItemViewModelBuilder itemViewModelBuilder,
            RequestModelAccessor requestModelAccessor,
            Litium.Accelerator.Services.ProductService productService,
            PageByFieldTemplateCache<BrandPageFieldTemplateCache> brandPageByFieldTypeCache,
            ProductFieldViewModelBuilder productFieldViewModelBuilder,
            ProductModelBuilder productModelBuilder,
            FieldDefinitionService fieldDefinitionService,
            ProductFieldGroupViewModelBuilder productFieldGroupViewModelBuilder,
            ProductPriceModelBuilder productPriceModelBuilder,
            CartContextAccessor cartContextAccessor,
            CountryService countryService,
            IEnumerable<PNProductPageModelService> pnProductPageModelService,
            PNProductPriceService pnProductPriceService) : base(
                itemViewModelBuilder,
            requestModelAccessor,
            productService,
            brandPageByFieldTypeCache,
            productModelBuilder,
            fieldDefinitionService,
            productFieldGroupViewModelBuilder)
        {
            _itemViewModelBuilder = itemViewModelBuilder;
            _requestModelAccessor = requestModelAccessor;
            _productService = productService;
            _brandPageByFieldTypeCache = brandPageByFieldTypeCache;
            _productModelBuilder = productModelBuilder;
            _fieldDefinitionService = fieldDefinitionService;
            _productFieldGroupViewModelBuilder = productFieldGroupViewModelBuilder;
            _pnProductPriceService = pnProductPriceService;
            _pnProductPageModelService = pnProductPageModelService;
        }

        public override async Task<ProductPageViewModel> BuildAsync(BaseProduct baseProduct)
        {
            var model = await base.BuildAsync(baseProduct);
            var viewModel = model.MapTo<PNProductPageViewModel>();
            var price = _pnProductPriceService.GetCorrectPrice(viewModel);
            foreach (var item in _pnProductPageModelService)
            {
                item.BuildPartialModel(ref viewModel, baseProduct, price);
            }

            return viewModel;
        }

        public override async Task<ProductPageViewModel> BuildAsync(Variant variant)
        {
            var model = await base.BuildAsync(variant);
            var viewModel = model.MapTo<PNProductPageViewModel>();

            var price = _pnProductPriceService.GetCorrectPrice(viewModel);
            foreach (var item in _pnProductPageModelService)
            {
                item.BuildPartialModel(ref viewModel, variant, price);
            }

            return viewModel;
        }
    }
}

