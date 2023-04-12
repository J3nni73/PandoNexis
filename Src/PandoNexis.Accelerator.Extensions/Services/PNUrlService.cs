using Litium.Globalization;
using Litium.Products;
using Litium.Runtime.DependencyInjection;
using Litium.Web;
using Litium.Web.Routing;
using Litium.Websites;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Channel = Litium.Globalization.Channel;
using Page = Litium.Websites.Page;

namespace PandoNexis.Accelerator.Extensions.Services
{
    [Service(ServiceType = typeof(PNUrlService))]
    public class PNUrlService
    {
        private readonly UrlService _urlService;
        public PNUrlService(UrlService urlService)
        {
            _urlService = urlService;
        }
        public string GetAbsoluteUrl(Channel channel, Page page, Category category, BaseProduct baseProduct, Variant variant)
        {
            var args = new ProductUrlArgs(channel.SystemId);
            args.AbsoluteUrl = true;

            if (variant != null)
            {
                return _urlService.GetUrl(variant, args);
            }

            if (baseProduct != null)
            {
                return _urlService.GetUrl(baseProduct, args);
            }

            if (category != null)
            {
                var cArgs = new CategoryUrlArgs(channel.SystemId);
                cArgs.AbsoluteUrl = true;
                return _urlService.GetUrl(category, cArgs);
            }

            if (page != null)
            {
                var pArgs = new PageUrlArgs(channel.SystemId);
                pArgs.AbsoluteUrl = true;
                pArgs.UsePrimaryDomainName = true;
                return _urlService.GetUrl(page, pArgs);
            }

            return String.Empty;
        }

        public string GetAbsoluteUrl(Channel channel, Variant variant)
        {
            return GetAbsoluteUrl(channel, null, null, null, variant);
        }
    }
}
