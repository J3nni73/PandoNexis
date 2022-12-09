using Litium.Accelerator.Extensions;
using Litium.Accelerator.Routing;
using Litium.Accelerator.ViewModels;
using Litium.Blocks;
using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Globalization;
using Litium.Media;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Web.Products.Routing;
using Litium.Web.Routing;
using Litium.Websites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Services
{
    [Service(ServiceType = typeof(PNFrameworkService), Lifetime = DependencyLifetime.Scoped)]
    [RequireServiceImplementation]
    public class PNFrameworkService
    {
        private readonly RequestModelAccessor _requestModelAccessor;
        private readonly RouteRequestInfoAccessor _routeRequestInfoAccessor;

        public PNFrameworkService(RouteRequestInfoAccessor routeRequestInfoAccessor, RequestModelAccessor requestModelAccessor)
        {
            _routeRequestInfoAccessor = routeRequestInfoAccessor;
            _requestModelAccessor = requestModelAccessor;
        }
        public string GetCurrentPageBodyCssClass()
        {
            var pageModel = _requestModelAccessor.RequestModel.CurrentPageModel;

            var pageModelName = pageModel.GetPageType() ?? string.Empty;
            var productPageData = _routeRequestInfoAccessor.RouteRequestInfo.Data as ProductPageData;
            if (productPageData?.TemplateId != null)
            {
                pageModelName = productPageData.TemplateId;
            }

            pageModelName = (Regex.Replace(pageModelName, "(?<=[a-z0-9])[A-Z]", "-$0", RegexOptions.Compiled)).ToLowerInvariant();
            return "p__" + pageModelName;
        }
    }
}
