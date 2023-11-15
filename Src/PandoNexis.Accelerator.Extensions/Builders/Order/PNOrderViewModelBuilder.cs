using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Accelerator.ViewModels.Order;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.AutoMapper;
using Litium.Sales;
using Litium.Tagging;
using Litium.Web;
using Litium.Web.Models.Products;
using Litium.Web.Models.Websites;
using Litium.Websites;
using Litium.StateTransitions;
using Litium.Sales.DiscountTypes;
using Litium.Runtime.DependencyInjection;
using Litium.Accelerator.Builders.Order;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.Accelerator.Extensions.ViewModels.Order;

namespace PandoNexis.Accelerator.Extensions.Builders.Order
{
    [Service(ServiceType = typeof(OrderViewModelBuilder), Lifetime = DependencyLifetime.Scoped)]
    public class PNOrderViewModelBuilder : OrderViewModelBuilder
    {
        private readonly HashSet<string> _discountTypes = new()
        {
            nameof(DiscountedProductPrice),
            nameof(ProductDiscount),
            nameof(BuyXGetCheapestDiscount),
            nameof(MixAndMatch)
        };

        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly FieldDefinitionService _fieldDefinitionService;
        private readonly CountryService _countryService;
        private readonly TaggingService _taggingService;
        private readonly OrderHelperService _orderHelperService;
        private readonly ShippingProviderService _shippingProviderService;
        private readonly StateTransitionsService _stateTransitionsService;
        private readonly PageService _pageServcie;
        private readonly UrlService _urlService;
        private readonly OrganizationService _organizationService;
        private readonly PersonStorage _personStorage;
        private readonly OrderOverviewService _orderOverviewService;
        private readonly ProductModelBuilder _productModelBuilder;
        private readonly VariantService _variantService;
        private readonly UnitOfMeasurementService _unitOfMeasurementService;
        private readonly ChannelService _channelService;
        private readonly CurrencyService _currencyService;
        private readonly IEnumerable<PNOrderModelService> _pnOrderModelService;
        private readonly IEnumerable<PNOrderDetailsModelService> _pnOrderDetailsModelService;

        public PNOrderViewModelBuilder(
            RequestModelAccessor requestModelAccessor,
            FieldDefinitionService fieldDefinitionService,
            PageService pageServcie,
            UrlService urlService,
            ProductModelBuilder productModelBuilder,
            VariantService variantService,
            UnitOfMeasurementService unitOfMeasurementService,
            OrganizationService organizationService,
            PersonStorage personStorage,
            OrderOverviewService orderOverviewService,
            ChannelService channelService,
            CurrencyService currencyService,
            ShippingProviderService shippingProviderService,
            StateTransitionsService stateTransitionsService,
            CountryService countryService,
            TaggingService taggingService,
            OrderHelperService orderHelperService,
            IEnumerable<PNOrderModelService> pnOrderModelService,
            IEnumerable<PNOrderDetailsModelService> pnOrderDetailsModelService,
            IsoCountryService isoCountryService) : base(requestModelAccessor, fieldDefinitionService, pageServcie, urlService,
                productModelBuilder, variantService, unitOfMeasurementService, organizationService, personStorage, orderOverviewService, channelService,
                currencyService, shippingProviderService, stateTransitionsService, countryService, taggingService, orderHelperService, isoCountryService)
        {
            _requestModelAccessor = requestModelAccessor;
            _fieldDefinitionService = fieldDefinitionService;
            _pageServcie = pageServcie;
            _urlService = urlService;

            _productModelBuilder = productModelBuilder;
            _variantService = variantService;
            _unitOfMeasurementService = unitOfMeasurementService;
            _organizationService = organizationService;
            _personStorage = personStorage;
            _orderOverviewService = orderOverviewService;
            _channelService = channelService;
            _currencyService = currencyService;
            _countryService = countryService;
            _taggingService = taggingService;
            _orderHelperService = orderHelperService;
            _shippingProviderService = shippingProviderService;
            _stateTransitionsService = stateTransitionsService;
            _pnOrderModelService = pnOrderModelService;
            _pnOrderDetailsModelService = pnOrderDetailsModelService;
        }

        public override OrderViewModel Build(Guid id, bool print)
            => Build(_requestModelAccessor.RequestModel.CurrentPageModel, id, print);

        public override OrderViewModel Build(PageModel pageModel, Guid id, bool print)
        {
            var model = base.Build(pageModel, id, print).MapTo<PNOrderViewModel>();
            foreach (var item in _pnOrderModelService)
            {
                item.BuildPartialModel(ref model);
            }

            return model;
        }

        public override OrderDetailsViewModel Build(OrderOverview orderOverview)
        {
            var model = base.Build(orderOverview).MapTo<PNOrderDetailsViewModel>();
            foreach (var item in _pnOrderDetailsModelService)
            {
                item.BuildPartialModel(ref model, orderOverview);
            }

            return model;
        }
    }
}
