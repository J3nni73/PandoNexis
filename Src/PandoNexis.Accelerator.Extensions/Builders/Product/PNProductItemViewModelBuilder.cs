using JetBrains.Annotations;
using Litium.Accelerator.Builders.Product;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Services;
using Litium.Accelerator.ViewModels.Product;
using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Sales;
using Litium.Web.Models;
using Litium.Web.Models.Products;
using PandoNexis.Accelerator.Extensions.Extensions;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.Accelerator.Extensions.ViewModels.Product;

namespace PandoNexis.Accelerator.Extensions.Builders.Product
{
    [UsedImplicitly]
    [Service(ServiceType = typeof(ProductItemViewModelBuilder), Lifetime = DependencyLifetime.Scoped)]
    public class PNProductItemViewModelBuilder : ProductItemViewModelBuilder
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly ProductPriceModelBuilder _productPriceModelBuilder;
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly StockService _stockService;
        private readonly ProductModelBuilder _productModelBuilder;
        private readonly CartContextAccessor _cartContextAccessor;
        private readonly CurrencyService _currencyService;
        private readonly CountryService _countryService;
        private readonly IEnumerable<PNProductItemModelService> _pnProductItemModelService;

        public PNProductItemViewModelBuilder(RequestModelAccessor requestModelAccessor,
            ProductPriceModelBuilder productPriceModelBuilder,
            FieldDefinitionService fieldDefinitionService,
            StockService stockService,
            ProductModelBuilder productModelBuilder,
            CartContextAccessor cartContextAccessor,
            CurrencyService currencyService,
            CountryService countryService,
            IEnumerable<PNProductItemModelService> pnProductItemModelService) : base(requestModelAccessor,
                                                productPriceModelBuilder,
                                                fieldDefinitionService,
                                                stockService,
                                                productModelBuilder,
                                                cartContextAccessor,
                                                currencyService,
                                                countryService)
        {
            _requestModelAccessor = requestModelAccessor;
            _productPriceModelBuilder = productPriceModelBuilder;
            _fieldDefinitionService = fieldDefinitionService;
            _stockService = stockService;
            _productModelBuilder = productModelBuilder;
            _cartContextAccessor = cartContextAccessor;
            _currencyService = currencyService;
            _countryService = countryService;
            _pnProductItemModelService = pnProductItemModelService;
        }
        public override ProductItemViewModel Build(Variant variant)
        {
            var productModel = _productModelBuilder.BuildFromVariant(variant);
            return productModel == null ? null : Build(productModel);
        }
        public override ProductItemViewModel Build(ProductModel productModel, bool inProductListPage = true, Category category = default)
        {
            var model = base.Build(productModel, inProductListPage, category);

            var viewModel = model.MapTo<PNProductItemViewModel>();
           
            var variant = productModel.SelectedVariant;
            if (variant != null)
            {
                viewModel.ExtendedDescription = variant.DescriptionExtended();
            }
            else
            {
                var baseProduct = productModel.BaseProduct;
                if (baseProduct != null)
                    viewModel.ExtendedDescription = baseProduct.DescriptionExtended();
            }
            if (!viewModel.Images.Any())
            {
                viewModel.Images = productModel.SelectedVariant.Fields.GetValue<IList<Guid>>(SystemFieldDefinitionConstants.Images).MapTo<IList<ImageModel>>() ??
                                    productModel.BaseProduct.Fields.GetValue<IList<Guid>>(SystemFieldDefinitionConstants.Images).MapTo<IList<ImageModel>>();
            }
            if (viewModel.Product == null)
            {
                viewModel.Product = productModel;
            }
            foreach (var item in _pnProductItemModelService)
            {
                item.BuildPartialModel(ref viewModel);
            }

            return viewModel;
        }
    }
}
