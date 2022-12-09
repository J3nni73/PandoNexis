using Litium.Runtime.DependencyInjection;

namespace PandoNexis.AddOns.Extensions.Definitions.WebsiteTexts
{ 
    [Service(ServiceType = typeof(WebsiteTextService))]
    [RequireServiceImplementation]
    public abstract class WebsiteTextService
    {
        public abstract void CreateWebsiteTexts(IWebsiteTextSource textSource);
        public abstract void DeleteMissingWebsiteTexts(IWebsiteTextSource textSource);
    }
}