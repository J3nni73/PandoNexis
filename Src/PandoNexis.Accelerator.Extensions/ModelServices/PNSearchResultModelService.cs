using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModels.Search;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNSearchResultModelService), Lifetime = DependencyLifetime.Scoped)]
    public abstract class PNSearchResultModelService : IPNModelService
    {
        public abstract void BuildPartialModelAsync(ref PNSearchResultViewModel model);
    }
}
