using Litium.Accelerator.Definitions;
using Litium.Runtime;
using Litium.Accelerator.Definitions.WebsiteTexts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solution.Extensions.SolutionSetup
{
    [Autostart]
    public class WebsiteTextSetup : IAsyncAutostart
    {
        private readonly WebsiteTextService _websiteTextService;
        private readonly IEnumerable<IWebsiteTextSource> _websiteTextSources;
        private readonly IEnumerable<IDefinitionSetup> _extraSetup;
        private readonly IEnumerable<IDefinitionInit> _extraInit;

        public WebsiteTextSetup(WebsiteTextService websiteTextService, IEnumerable<IWebsiteTextSource> websiteTextSources, IEnumerable<IDefinitionInit> extraInit, IEnumerable<IDefinitionSetup> extraSetup)
        {
            _websiteTextService = websiteTextService;
            _websiteTextSources = websiteTextSources;
            _extraInit = extraInit;
            _extraSetup = extraSetup;
        }

        private void Start()
        {
            // All definitions matching above prefix that are no longer in coded definitions are deleted from database
            foreach (var textSource in _websiteTextSources)
            {
                //_websiteTextService.DeleteMissingWebsiteTexts(textSource);

                // Add any new text in coded definitions to database
                _websiteTextService.CreateWebsiteTexts(textSource);
            }
        }

        public async ValueTask StartAsync(CancellationToken cancellationToken)
        {
            foreach (var item in _extraInit)
            {
                await item.StartAsync(cancellationToken).ConfigureAwait(false);
            }

            Start();

            foreach (var item in _extraSetup)
            {
                await item.StartAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }
}