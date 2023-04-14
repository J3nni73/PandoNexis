using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.Framework.ViewModels;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNHeaderModelService))]
    public abstract class PNHeaderModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNHeaderViewModel model);

    }
}
