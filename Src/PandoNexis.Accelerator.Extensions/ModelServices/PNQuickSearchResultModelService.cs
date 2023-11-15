using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ViewModels.Search;

namespace PandoNexis.Accelerator.Extensions.ModelServices
{
    [Service(ServiceType = typeof(PNQuickSearchResultModelService), Lifetime = DependencyLifetime.Scoped)]
    public abstract class PNQuickSearchResultModelService : IPNModelService
    {
        public abstract void BuildPartialModel(ref PNQuickSearchResultViewModel model, string query);
    }
}
