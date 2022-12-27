using Litium.Accelerator.Routing;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNInfiniteScroll.Constants;

namespace PandoNexis.AddOns.Extensions.PNInfinityScroll.Services
{
    [Service(ServiceType = typeof(InfinityScrollService), Lifetime = DependencyLifetime.Scoped)]
    [RequireServiceImplementation]
    public class InfinityScrollService
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        public InfinityScrollService(RequestModelAccessor requestModelAccessor) { 
            _requestModelAccessor = requestModelAccessor;
        }
        public string GetPaginationType()
        {
            var website = _requestModelAccessor.RequestModel.WebsiteModel;
            var paginationType = website.GetValue<String>(PNInfiniteScrollConstants.PaginationType);

            return website.GetValue<String>(PNInfiniteScrollConstants.PaginationType)??string.Empty;
        }
    }
}
