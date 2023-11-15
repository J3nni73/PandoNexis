using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModels.Order;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNOrderConfirmationModelService), Lifetime = DependencyLifetime.Scoped)]
    public abstract class PNOrderConfirmationModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNOrderConfirmationViewModel model);
    }
}
