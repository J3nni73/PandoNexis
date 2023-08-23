using Litium.Runtime.DependencyInjection;
using Newtonsoft.Json;
using PandoNexis.AddOns.PNPilot.Objects;
using PandoNexis.AddOns.PNPilot.Services.DALServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.PNPilot.Services
{
    [Service(ServiceType = typeof(ItemStatusService))]
    public class ItemStatusService
    {
        private readonly ItemStatusDALService _itemStatusDALService;
        public ItemStatusService(ItemStatusDALService itemStatusDALService)
        {
            _itemStatusDALService = itemStatusDALService;
        }

        public IEnumerable<ItemStatus> GetItemStatuses()
        {
            return _itemStatusDALService.GetAll();
        }

        public bool AddOrUpdateTimeType(string jsonItem)
        {
            var item = JsonConvert.DeserializeObject<ItemStatus>(jsonItem);
            if (item == null) return false;
            return _itemStatusDALService.AddOrUpdate(item);

        }

    }
}
