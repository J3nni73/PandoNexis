using Litium.Runtime.DependencyInjection;
using Newtonsoft.Json;
using Solution.Extensions.PNPilot.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Extensions.PNPilot.Services
{
    [Service(ServiceType = typeof(PilotItemService))]
    public class PilotItemService
    {
        private readonly PilotItemDALService _pilotItemDALService;
        public PilotItemService(PilotItemDALService pilotItemDALService)
        {
            _pilotItemDALService = pilotItemDALService;
        }

        public List<Item> GetItems()
        {
            return _pilotItemDALService.GetItems();
        }

        public bool AddOrUpdateItem(string jsonItem)
        {
            var item = JsonConvert.DeserializeObject<Item>(jsonItem);
            if (item == null) return false;
            return _pilotItemDALService.AddOrUpdateItem(item);

        }

    }
}
