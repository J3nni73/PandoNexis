using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModels.Framework;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNClientContextModelService), Lifetime = DependencyLifetime.Scoped)]
    public abstract class PNClientContextModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNClientContextViewModel model);

    }
}
