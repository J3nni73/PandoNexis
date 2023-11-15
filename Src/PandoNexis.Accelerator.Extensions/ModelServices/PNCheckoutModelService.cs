using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModels.Checkout;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNCheckoutModelService), Lifetime = DependencyLifetime.Scoped)]
    public abstract class PNCheckoutModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNCheckoutViewModel model);
    }
}
