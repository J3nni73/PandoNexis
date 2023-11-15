using Litium.Accelerator.Builders.Search;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Searchers;
using Litium.Accelerator.ViewModels.Search;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using PandoNexis.Accelerator.Extensions.ModelServices;
using PandoNexis.Accelerator.Extensions.ViewModels.Search;

namespace PandoNexis.Accelerator.Extensions.Builders.Search
{
    [Service(ServiceType = typeof(QuickSearchResultViewModelBuilder), Lifetime = DependencyLifetime.Scoped)]
    public class PNQuickSearchResultViewModelBuilder : QuickSearchResultViewModelBuilder
    {
        private readonly IEnumerable<BaseSearcher> _searchers;
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly IEnumerable<PNQuickSearchResultModelService> _pnQuickSearchResultModelService;

        public PNQuickSearchResultViewModelBuilder(IEnumerable<BaseSearcher> searchers, RequestModelAccessor requestModelAccessor, IEnumerable<PNQuickSearchResultModelService> pnQuickSearchResultModelService)
            : base(searchers, requestModelAccessor)
        {
            _searchers = searchers;
            _requestModelAccessor = requestModelAccessor;
            _pnQuickSearchResultModelService = pnQuickSearchResultModelService;
        }

        public override async Task<QuickSearchResultViewModel> BuildAsync(string query)
        {
            var model = await base.BuildAsync(query);
            var viewModel = model.MapTo<PNQuickSearchResultViewModel>();

            foreach (var item in _pnQuickSearchResultModelService)
            {
                item.BuildPartialModel(ref viewModel, query);
            }

            return viewModel;
        }
    }
}
