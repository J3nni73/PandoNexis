﻿using Litium.Accelerator.Definitions;
using Litium.Runtime;
using Litium.FieldFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Litium.Security;
using PandoNexis.AddOns.Extensions.PNFilesToFieldConnect;
using PandoNexis.Accelerator.Extensions.Database.Services;

namespace Solution.Extensions.SolutionSetup
{
    [Autostart]
    public class Startup : IAsyncAutostart
    {

        private readonly IEnumerable<IDefinitionSetup> _extraSetup;
        private readonly IEnumerable<IDefinitionInit> _extraInit;
        private readonly SecurityContextService _securityContextService;
        private readonly IEnumerable<DatabaseInitiator> _databaseInitiator;

        public Startup(
            IEnumerable<IDefinitionInit> extraInit,
            IEnumerable<IDefinitionSetup> extraSetup,
            SecurityContextService securityContextService,
            IEnumerable<DatabaseInitiator> databaseInitiator)
        {
            _extraInit = extraInit;
            _extraSetup = extraSetup;
            _securityContextService = securityContextService;
            _databaseInitiator = databaseInitiator;
        }

        public void Start()
        {
            using (_securityContextService.ActAsSystem("setup"))
            {
                foreach (var item in _databaseInitiator)
                {
                    item.GetCheckDatabaseObjects();
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
