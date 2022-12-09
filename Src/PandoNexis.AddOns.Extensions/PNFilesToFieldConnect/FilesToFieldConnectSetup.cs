using Litium.Accelerator.Definitions;
using Litium.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNFilesToFieldConnect
{
    [Autostart]
    public class DefinitionSetup : IAsyncAutostart
    {

        private readonly IEnumerable<IDefinitionSetup> _extraSetup;
        private readonly IEnumerable<IDefinitionInit> _extraInit;
        private readonly FilesToFieldConnectService _filesToFieldConnectService;

        public DefinitionSetup(FilesToFieldConnectService filesToFieldConnect,
            IEnumerable<IDefinitionInit> extraInit,
            IEnumerable<IDefinitionSetup> extraSetup)
        {
            _filesToFieldConnectService = filesToFieldConnect;
            _extraInit = extraInit;
            _extraSetup = extraSetup;
        }

        public void Start()
        {
            _filesToFieldConnectService.InitiateProductCertificateService(FilesToFieldConnectConstants.ProductCertificates);
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
