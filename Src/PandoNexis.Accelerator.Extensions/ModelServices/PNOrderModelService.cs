using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModels.Order;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNOrderModelService))]
    public abstract class PNOrderModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNOrderViewModel model);

    }
}
