using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModel.Product;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNProductItemModelService))]
    public abstract class PNProductItemModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNProductItemViewModel model);

    }
}
