using Litium.Accelerator.Definitions;
using Litium.Runtime;
using Litium.Security;
using PandoNexis.Accelerator.Extensions.Database.Services;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;

namespace Solution.Extensions.SolutionSetup
{
    [Autostart]
    public class Startup : IAsyncAutostart
    {

        private readonly IEnumerable<IDefinitionSetup> _extraSetup;
        private readonly IEnumerable<IDefinitionInit> _extraInit;
        private readonly SecurityContextService _securityContextService;
        private readonly IEnumerable<DatabaseInitiator> _databaseInitiator;
        private readonly IEnumerable<FieldHelper> _fieldHelper;

        public Startup(
            IEnumerable<IDefinitionInit> extraInit,
            IEnumerable<IDefinitionSetup> extraSetup,
            SecurityContextService securityContextService,
            IEnumerable<DatabaseInitiator> databaseInitiator,
            IEnumerable<FieldHelper> fieldHelper)
        {
            _extraInit = extraInit;
            _extraSetup = extraSetup;
            _securityContextService = securityContextService;
            _databaseInitiator = databaseInitiator;
            _fieldHelper = fieldHelper;
        }

        public void Start()
        {
            using (_securityContextService.ActAsSystem("setup"))
            {
                foreach (var item in _databaseInitiator)
                {
                    item.GetCheckDatabaseObjects();
                }

                foreach(var item in _fieldHelper)
                {
                    item.HandleFieldOptions();
                    item.HandleMultiFieldFields();
                }

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
