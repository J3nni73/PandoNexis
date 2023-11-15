using Litium.Accelerator.Routing;
using Litium.Accelerator.ViewModels.Product;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Sales;
using Litium.Web.Models.Products;
using Litium.Web.Routing;
using PandoNexis.Accelerator.Extensions.ViewModels.Product;

namespace PandoNexis.Accelerator.Extensions.Services.Product
{
    [Service(ServiceType = typeof(PNProductPriceService), Lifetime = DependencyLifetime.Scoped)]
    [RequireServiceImplementation]
    public class PNProductPriceService
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly RouteRequestInfoAccessor _routeRequestInfoAccessor;
        private readonly ProductPriceModelBuilder _productPriceModelBuilder;
        private readonly CartContextAccessor _cartContextAccessor;
        private readonly CountryService _countryService;
        private readonly VariantService _variantService;

        public PNProductPriceService(RouteRequestInfoAccessor routeRequestInfoAccessor, RequestModelAccessor requestModelAccessor, ProductPriceModelBuilder productPriceModelBuilder, CartContextAccessor cartContextAccessor, CountryService countryService, VariantService variantService)
        {
            _routeRequestInfoAccessor = routeRequestInfoAccessor;
            _requestModelAccessor = requestModelAccessor;
            _productPriceModelBuilder = productPriceModelBuilder;
            _cartContextAccessor = cartContextAccessor;
            _countryService = countryService;
            _variantService = variantService;
        }

        public async Task<decimal?> GetCorrectAsyncPrice(ProductItemViewModel productItem)
        {
            if (productItem == null)
            {
                return null;
            }
            var variant = productItem?.Product?.SelectedVariant;
            if (variant == null)
            {
                variant = _variantService.Get(productItem.Id);
            }
            var country = (_cartContextAccessor.CartContext == null) ? _requestModelAccessor.RequestModel.CountryModel.Country
            : _countryService.Get(_cartContextAccessor.CartContext.CountryCode);
            return _productPriceModelBuilder.Build(variant, productItem.Currency, _requestModelAccessor.RequestModel.ChannelModel.Channel, country).Price.Price;
        }
        public decimal? GetCorrectPrice(ProductItemViewModel productItem)
        {
            if (productItem == null)
            {
                return null;
            }
            var variant = productItem?.Product?.SelectedVariant;
            if (variant == null)
            {
                variant = _variantService.Get(productItem.Id);
            }
            var country = (_cartContextAccessor.CartContext == null) ? _requestModelAccessor.RequestModel.CountryModel.Country
            : _countryService.Get(_cartContextAccessor.CartContext.CountryCode);
            return _productPriceModelBuilder.Build(variant, productItem.Currency, _requestModelAccessor.RequestModel.ChannelModel.Channel, country).Price.Price;
        }

        public decimal? GetCorrectPrice(PNProductItemViewModel productItem)
        {
            if (productItem == null)
            {
                return null;
            }
            var variant = productItem?.Product?.SelectedVariant;
            if (variant == null)
            {
                variant = _variantService.Get(productItem.Id);
            }
            var country = (_cartContextAccessor.CartContext == null) ? _requestModelAccessor.RequestModel.CountryModel.Country
            : _countryService.Get(_cartContextAccessor.CartContext.CountryCode);
            return _productPriceModelBuilder.Build(variant, productItem.Currency, _requestModelAccessor.RequestModel.ChannelModel.Channel, country)?.Price?.Price ?? 0;
        }

        public decimal? GetCorrectPrice(ProductPageViewModel viewModel)
        {
            var variant = viewModel.ProductItem?.Product?.SelectedVariant;
            if (variant == null)
            {
                if (viewModel.ProductItem == null)
                {
                    return null;
                }
                variant = _variantService.Get(viewModel.ProductItem.Id);
            }
            var country = (_cartContextAccessor.CartContext == null) ? _requestModelAccessor.RequestModel.CountryModel.Country
            : _countryService.Get(_cartContextAccessor.CartContext.CountryCode);
            return _productPriceModelBuilder.Build(variant, viewModel.ProductItem.Currency, _requestModelAccessor.RequestModel.ChannelModel.Channel, country)?.Price?.Price ?? 0;

        }
        public decimal? GetCorrectPrice(PNProductPageViewModel viewModel)
        {
            var variant = viewModel.ProductItem?.Product?.SelectedVariant;
            if (variant == null)
            {
                if (viewModel.ProductItem == null)
                {
                    return null;
                }
                variant = _variantService.Get(viewModel.ProductItem.Id);
            }

            var country = (_cartContextAccessor.CartContext == null) ? _requestModelAccessor.RequestModel.CountryModel.Country
            : _countryService.Get(_cartContextAccessor.CartContext.CountryCode);
            return _productPriceModelBuilder.Build(variant, viewModel.ProductItem.Currency, _requestModelAccessor.RequestModel.ChannelModel.Channel, country)?.Price?.Price ?? 0;
        }
    }
}
