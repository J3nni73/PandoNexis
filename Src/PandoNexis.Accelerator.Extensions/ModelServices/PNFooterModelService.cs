using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.Framework.ViewModels;
using PandoNexis.Accelerator.Extensions.ViewModels.Search;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNFooterModelService), Lifetime = DependencyLifetime.Scoped)]
    public abstract class PNFooterModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNFooterViewModel model);
    }
}
