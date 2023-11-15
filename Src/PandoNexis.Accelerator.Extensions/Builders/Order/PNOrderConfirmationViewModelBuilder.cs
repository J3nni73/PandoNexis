using Litium.Accelerator.Builders.Checkout;
using Litium.Accelerator.Builders.Order;
using Litium.Accelerator.ViewModels.Order;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Sales;
using Litium.Web.Models.Websites;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.Accelerator.Extensions.ViewModels.Order;

namespace PandoNexis.Accelerator.Extensions.Builders.Order
{
    [Service(ServiceType = typeof(OrderConfirmationViewModelBuilder), Lifetime = DependencyLifetime.Scoped)]
    public class PNOrderConfirmationViewModelBuilder : OrderConfirmationViewModelBuilder
    {
        private readonly OrderViewModelBuilder _orderViewModelBuilder;
        private readonly OrderOverviewService _orderOverviewService;
        private readonly CartContextAccessor _cartContextAccessor;
        private readonly IEnumerable<PNOrderConfirmationModelService> _pnOrderConfirmationModelService;

        public PNOrderConfirmationViewModelBuilder(
            OrderViewModelBuilder orderViewModelBuilder,
            OrderOverviewService orderOverviewService,
            CartContextAccessor cartContextAccessor,
            IEnumerable<PNOrderConfirmationModelService> pnOrderConfirmationModelService) : base(orderViewModelBuilder,
            orderOverviewService,
            cartContextAccessor)
        {
            _orderViewModelBuilder = orderViewModelBuilder;
            _orderOverviewService = orderOverviewService;
            _cartContextAccessor = cartContextAccessor;
            _pnOrderConfirmationModelService = pnOrderConfirmationModelService;
        }

        public override OrderConfirmationViewModel Build(PageModel pageModel, Guid orderSystemId)
        {
            var viewModel = base.Build(pageModel, orderSystemId).MapTo<PNOrderConfirmationViewModel>();

            foreach (var item in _pnOrderConfirmationModelService)
            {
                item.BuildPartialModel(ref viewModel);
            }

            return viewModel;
        }
    }
}
