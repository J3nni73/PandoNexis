using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModels.Product;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNCategoryPageModelService), Lifetime = DependencyLifetime.Scoped)]
    public abstract class PNCategoryPageModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNCategoryPageViewModel model);
    }
}
