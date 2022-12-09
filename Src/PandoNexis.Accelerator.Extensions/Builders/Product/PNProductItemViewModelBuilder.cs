using JetBrains.Annotations;
using Litium.Accelerator.Builders.Product;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Services;
using Litium.Accelerator.ViewModels.Product;
using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Sales;
using Litium.Web.Models.Products;
using PandoNexis.Accelerator.Extensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public PNProductItemViewModelBuilder(RequestModelAccessor requestModelAccessor,
            ProductPriceModelBuilder productPriceModelBuilder,
            FieldDefinitionService fieldDefinitionService,
            StockService stockService,
            ProductModelBuilder productModelBuilder,
            CartContextAccessor cartContextAccessor,
            CurrencyService currencyService,
            CountryService countryService) : base(requestModelAccessor,
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
        }
        public override ProductItemViewModel Build(Variant variant)
        {
            var productModel = _productModelBuilder.BuildFromVariant(variant);
            return productModel == null ? null : Build(productModel);
        }
        public override ProductItemViewModel Build(ProductModel productModel, bool inProductListPage = true, Category category = default)
        {
            var model = base.Build(productModel, inProductListPage, category);
            var variant = productModel.SelectedVariant;
            if (variant != null)
            {
                model.ExtendedDescription = variant.DescriptionExtended();
            }
            else
            {
                var baseProduct = productModel.BaseProduct;
                if (baseProduct != null)
                    model.ExtendedDescription = baseProduct.DescriptionExtended();
            }
            return model;
        }

    }
}
