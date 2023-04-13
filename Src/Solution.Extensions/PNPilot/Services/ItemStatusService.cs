using Litium.Runtime.DependencyInjection;
using Newtonsoft.Json;
using Solution.Extensions.PNPilot.Objects;
using Solution.Extensions.PNPilot.Services.DALServices;

namespace Solution.Extensions.PNPilot.Services
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
