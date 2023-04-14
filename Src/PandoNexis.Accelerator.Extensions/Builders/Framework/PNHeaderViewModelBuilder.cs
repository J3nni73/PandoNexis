using Litium.Accelerator.Builders.Framework;
using Litium.Accelerator.Caching;
using Litium.Accelerator.Routing;
using Litium.Accelerator.Utilities;
using Litium.Accelerator.ViewModels.Framework;
using Litium.Runtime.AutoMapper;
using Litium.Runtime.DependencyInjection;
using Litium.Security;
using Litium.Web;
using Litium.Web.Routing;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Framework.ViewModels;
using PandoNexis.Accelerator.Extensions.ModelServices;

namespace PandoNexis.Accelerator.Extensions.Builders.Framework
{
    [Service(ServiceType = typeof(HeaderViewModelBuilder<HeaderViewModel>), Lifetime = DependencyLifetime.Scoped)]
    public class
         PNHeaderViewModelBuilder : HeaderViewModelBuilder<HeaderViewModel>
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly PageService _pageService;
        private readonly UrlService _urlService;
        private readonly PersonStorage _personStorage;
        private readonly SecurityContextService _securityContextService;
        private readonly IEnumerable<PNHeaderModelService> _pnHeaderModelService;

        public PNHeaderViewModelBuilder(
            RequestModelAccessor requestModelAccessor,
            PageService pageService, UrlService urlService,
            RouteRequestLookupInfoAccessor routeRequestLookupInfoAccessor,
            PageByFieldTemplateCache<LoginPageByFieldTemplateCache> pageByFieldType,
            SecurityContextService securityContextService, IEnumerable<PNHeaderModelService> pnHeaderModelService) : base(urlService,
                                                                    requestModelAccessor,
                                                                    pageByFieldType,
                                                                    pageService,
                                                                    routeRequestLookupInfoAccessor)
        {
            _requestModelAccessor = requestModelAccessor;
            _pageService = pageService;
            _urlService = urlService;
            _securityContextService = securityContextService;
            _pnHeaderModelService = pnHeaderModelService;
        }

        public override HeaderViewModel Build()
        {
            var viewModel = base.Build().MapTo<PNHeaderViewModel>();

            foreach (var item in _pnHeaderModelService)
            {
                item.BuildPartialModel(ref viewModel);
            }

            return viewModel;
        }
    }
}
