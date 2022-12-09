using Litium.Accelerator.Definitions;
using Litium.Runtime;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.Definitions.WebsiteTexts
{
    [Autostart]
    public class WebsiteTextSetup : IAsyncAutostart
    {
        private readonly WebsiteTextService _websiteTextService;
        private readonly IWebsiteTextSource _websiteTextSource;
        private readonly IEnumerable<IDefinitionSetup> _extraSetup;
        private readonly IEnumerable<IDefinitionInit> _extraInit;

        public WebsiteTextSetup(WebsiteTextService websiteTextService, IWebsiteTextSource websiteTextSource, IEnumerable<IDefinitionInit> extraInit, IEnumerable<IDefinitionSetup> extraSetup)
        {
            _websiteTextService = websiteTextService;
            _websiteTextSource = websiteTextSource;
            _extraInit = extraInit;
            _extraSetup = extraSetup;
        }

        private void Start()
        {
            // All definitions matching above prefix that are no longer in coded definitions are deleted from database
            _websiteTextService.DeleteMissingWebsiteTexts(_websiteTextSource);

            // Add any new text in coded definitions to database
            _websiteTextService.CreateWebsiteTexts(_websiteTextSource);
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