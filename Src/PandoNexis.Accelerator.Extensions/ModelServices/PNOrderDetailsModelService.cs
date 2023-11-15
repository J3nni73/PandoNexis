using Litium.Accelerator.ViewModels.Order;
using Litium.Runtime.DependencyInjection;
using Litium.Sales;
using PandoNexis.Accelerator.Extensions.ViewModels.Order;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNOrderDetailsModelService))]
    public abstract class PNOrderDetailsModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNOrderViewModel model);
        public abstract void BuildPartialModel(ref PNOrderDetailsViewModel model, OrderOverview orderOverview);
    }
}
