using Litium.Accelerator.Builders.Framework;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Search.Filtering;
using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Web;
using Litium.Web.Routing;
using PandoNexis.Accelerator.Extensions.ViewModels.Framework;
using PandoNexis.Accelerator.Extensions.ModelServices;

namespace Solution.Extensions.Builders.Framework
{
    [Litium.Owin.UsedImplicitly]
    [Service(ServiceType = typeof(HeadViewModelBuilder<HeadViewModel>), Lifetime = DependencyLifetime.Default)]

    public class PNExtendedHeadViewModelBuilder : HeadViewModelBuilder<HeadViewModel>
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly IEnumerable<PNHeadModelService> _pnHeadModelService;

        public PNExtendedHeadViewModelBuilder(RouteRequestInfoAccessor routeRequestInfoAccessor, RequestModelAccessor requestModelAccessor, MetaService metaService, OpenGraphViewModelBuilder openGraphViewModelBuilder, MetaService.CanonicalSettings canonicalSettings, MetaService.RobotsSettings robotsSettings, TrackingScriptService trackingScriptService, FaviconViewModelBuilder faviconViewModelBuilder, FilterService filterService, IEnumerable<PNHeadModelService> pnHeadModelService)
        : base(routeRequestInfoAccessor, requestModelAccessor, metaService, openGraphViewModelBuilder, canonicalSettings, robotsSettings, trackingScriptService, faviconViewModelBuilder, filterService)
        {
            _requestModelAccessor = requestModelAccessor;
            _pnHeadModelService = pnHeadModelService;
        }

        public override HeadViewModel Build()
        {
            var model = base.Build().MapTo<PNHeadViewModel>();
            foreach (var item in _pnHeadModelService)
            {
                item.BuildPartialModel(ref model);
            }

            return model;
        }
    }
}
