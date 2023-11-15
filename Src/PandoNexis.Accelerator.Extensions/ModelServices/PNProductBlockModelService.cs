using Litium.Accelerator.ViewModels.Block;
using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModels.Block;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNProductBlockModelService), Lifetime = DependencyLifetime.Transient)]
    public abstract class PNProductBlockModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNProductBlockViewModel model, ProductBlockViewModelData data);
    }
}
