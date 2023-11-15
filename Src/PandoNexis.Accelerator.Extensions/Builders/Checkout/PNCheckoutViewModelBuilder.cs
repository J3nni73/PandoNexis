using Litium.Accelerator.Builders.Checkout;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Accelerator.ViewModels.Checkout;
using Litium.Customers;
using Litium.Globalization;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Sales;
using Litium.Security;
using Litium.Web.Routing;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.Accelerator.Extensions.ViewModels.Checkout;

namespace PandoNexis.Accelerator.Extensions.Builders.Checkout
{
    [Service(ServiceType = typeof(CheckoutViewModelBuilder), Lifetime = DependencyLifetime.Scoped)]
    public class PNCheckoutViewModelBuilder : CheckoutViewModelBuilder
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly RouteRequestLookupInfoAccessor _routeRequestLookupInfoAccessor;
        private readonly SecurityContextService _securityContextService;
        private readonly DeliveryMethodViewModelBuilder _deliveryMethodViewModelBuilder;
        private readonly PaymentOptionViewModelBuilder _paymentOptionViewModelBuilder;
        private readonly PersonService _personService;
        private readonly AddressTypeService _addressTypeService;
        private readonly PersonStorage _personStorage;
        private readonly ISignInUrlResolver _signInUrlResolver;
        private readonly CountryService _countryService;
        private readonly PaymentService _paymentService;
        private readonly CurrencyService _currencyService;
        private readonly CartContextAccessor _cartContextAccessor;
        private readonly IEnumerable<PNCheckoutModelService> _pnCheckoutModelService;

        public PNCheckoutViewModelBuilder(
            RequestModelAccessor requestModelAccessor,
            RouteRequestLookupInfoAccessor routeRequestLookupInfoAccessor,
            SecurityContextService securityContextService,
            DeliveryMethodViewModelBuilder deliveryMethodViewModelBuilder,
            PaymentOptionViewModelBuilder paymentOptionViewModelBuilder,
            PersonService personService,
            ISignInUrlResolver signInUrlResolver,
            AddressTypeService addressTypeService,
            CountryService countryService,
            PersonStorage personStorage,
            PaymentService paymentService,
            CurrencyService currencyService,
            CartContextAccessor cartContextAccessor,
            IEnumerable<PNCheckoutModelService> pnCheckoutModelService) : base(
            requestModelAccessor,
            routeRequestLookupInfoAccessor,
            securityContextService,
            deliveryMethodViewModelBuilder,
            paymentOptionViewModelBuilder,
            personService,
            signInUrlResolver,
            addressTypeService,
            countryService,
            personStorage,
            paymentService,
            currencyService,
            cartContextAccessor)
        {
            _requestModelAccessor = requestModelAccessor;
            _routeRequestLookupInfoAccessor = routeRequestLookupInfoAccessor;
            _securityContextService = securityContextService;
            _deliveryMethodViewModelBuilder = deliveryMethodViewModelBuilder;
            _paymentOptionViewModelBuilder = paymentOptionViewModelBuilder;
            _personService = personService;
            _addressTypeService = addressTypeService;
            _countryService = countryService;
            _personStorage = personStorage;
            _signInUrlResolver = signInUrlResolver;
            _paymentService = paymentService;
            _currencyService = currencyService;
            _cartContextAccessor = cartContextAccessor;
            _pnCheckoutModelService = pnCheckoutModelService;
        }

        public override async Task<CheckoutViewModel> BuildAsync(CartContext cartContext)
        {
            var model = await base.BuildAsync(cartContext);
            var viewModel = model.MapTo<PNCheckoutViewModel>();

            foreach (var item in _pnCheckoutModelService)
            {
                item.BuildPartialModel(ref viewModel);
            }

            return viewModel;
        }
    }
}
