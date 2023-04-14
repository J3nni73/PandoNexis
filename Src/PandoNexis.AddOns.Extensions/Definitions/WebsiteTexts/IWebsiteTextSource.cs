using Litium.Runtime.DependencyInjection;

namespace PandoNexis.AddOns.Extensions.Definitions.WebsiteTexts
{
    [Service(ServiceType = typeof(IWebsiteTextSource))]
    [RequireServiceImplementation]
    public interface IWebsiteTextSource
    {
        string Prefix { get; }

        bool UpdateExistingTexts { get; }

        List<WebsiteTextDefinition> GetTexts();
    }
}