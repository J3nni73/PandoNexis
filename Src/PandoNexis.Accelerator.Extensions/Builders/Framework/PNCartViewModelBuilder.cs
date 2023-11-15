using Litium.Accelerator.Builders.Framework;
using Litium.Accelerator.Routing;
using Litium.Accelerator.ViewModels.Framework;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Sales;
using Litium.Sales.DiscountTypes;
using Litium.Web;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.Accelerator.Extensions.ViewModels.Framework;

namespace PandoNexis.Accelerator.Extensions.Builders.Framework
{
    [Service(ServiceType = typeof(CartViewModelBuilder), Lifetime = DependencyLifetime.Scoped)]
    public class PNCartViewModelBuilder : CartViewModelBuilder
    {
        private readonly HashSet<string> _discountTypes = new()
        {
            nameof(DiscountedProductPrice),
            nameof(ProductDiscount),
            nameof(BuyXGetCheapestDiscount),
            nameof(MixAndMatch)
        };

        private record ProductDiscountMap(decimal ExcludingVat, decimal IncludingVat);

        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly UrlService _urlService;
        private readonly ChannelService _channelService;
        private readonly CurrencyService _currencyService;
        private readonly BaseProductService _baseProductService;
        private readonly VariantService _variantService;
        private readonly UnitOfMeasurementService _unitOfMeasurementService;
        private readonly IEnumerable<PNCartModelService> _pnCartModelService;

        public PNCartViewModelBuilder(
            RequestModelAccessor requestModelAccessor,
            UrlService urlService,
            ChannelService channelService,
            CurrencyService currencyService,
            BaseProductService baseProductService,
            VariantService variantService,
            UnitOfMeasurementService unitOfMeasurementService,
            IEnumerable<PNCartModelService> pnCartModelService) : base(requestModelAccessor,
                                                                    urlService,
                                                                    channelService,
                                                                    currencyService,
                                                                    baseProductService, variantService, unitOfMeasurementService)
        {
            _requestModelAccessor = requestModelAccessor;
            _urlService = urlService;
            _channelService = channelService;
            _currencyService = currencyService;
            _baseProductService = baseProductService;
            _variantService = variantService;
            _unitOfMeasurementService = unitOfMeasurementService;
            _pnCartModelService = pnCartModelService;
        }

        public override CartViewModel Build(CartContext cartContext)
        {
            var viewModel = base.Build(cartContext).MapTo<PNCartViewModel>();
         
            foreach (var item in _pnCartModelService)
            {
                item.BuildPartialModel(ref viewModel);
            }

            return viewModel;
        }
        
    }
}
