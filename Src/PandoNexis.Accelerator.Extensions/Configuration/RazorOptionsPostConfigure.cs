using JetBrains.Annotations;
using Litium.Runtime.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;

namespace PandoNexis.Accelerator.Extensions.Configuration
{
    [Service(ServiceType = typeof(IConfigureOptions<RazorViewEngineOptions>), Lifetime = DependencyLifetime.Transient)]
    [UsedImplicitly]
    public class RazorOptionsPostConfigure : IConfigureOptions<RazorViewEngineOptions>
    {
        public void Configure(RazorViewEngineOptions options)
        {
            options.ViewLocationFormats.Insert(0, "/Views/_PandoNexis/Blocks/{0}" + RazorViewEngine.ViewExtension);
            options.ViewLocationFormats.Insert(0, "/Views/_PandoNexis/Shared/{0}" + RazorViewEngine.ViewExtension);
            options.ViewLocationFormats.Insert(0, "/Views/_PandoNexis/{1}/{0}" + RazorViewEngine.ViewExtension);

            options.ViewLocationFormats.Insert(0, "/Views/_Addons/Blocks/{0}" + RazorViewEngine.ViewExtension);
            options.ViewLocationFormats.Insert(0, "/Views/_Addons/Shared/{0}" + RazorViewEngine.ViewExtension);
            options.ViewLocationFormats.Insert(0, "/Views/_Addons/{1}/{0}" + RazorViewEngine.ViewExtension);

            options.ViewLocationFormats.Insert(0, "/Views/_Solution/Blocks/{0}" + RazorViewEngine.ViewExtension);
            options.ViewLocationFormats.Insert(0, "/Views/_Solution/Shared/{0}" + RazorViewEngine.ViewExtension);
            options.ViewLocationFormats.Insert(0, "/Views/_Solution/{1}/{0}" + RazorViewEngine.ViewExtension);
        }
    }
}