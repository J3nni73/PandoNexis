using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.Pilot.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.Pilot.Services
{
    [Service(ServiceType = typeof(PilotItemService))]
    public class PilotItemService
    {
        private readonly PilotItemDALService  _pilotItemDALService;
        public PilotItemService(PilotItemDALService pilotItemDALService)
        {
            _pilotItemDALService = pilotItemDALService;
        }

        public List<Item> GetItems()
        {
            return _pilotItemDALService.GetItems();
        }
    }
}
