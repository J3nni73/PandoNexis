using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModels.Framework;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNCartModelService))]
    public abstract class PNCartModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNCartViewModel model);
    }
}
